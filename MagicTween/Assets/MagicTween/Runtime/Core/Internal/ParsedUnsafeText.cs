using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace MagicTween.Core
{
    [BurstCompile]
    public struct ParsedUnsafeText : IDisposable
    {
        public ParsedUnsafeText(UnsafeText source, bool richTextEnabled, Allocator allocator)
        {
            length = source.Length;

            if (!richTextEnabled)
            {
                symbols = new UnsafeList<TextSymbol>(1, allocator);
                var text = new UnsafeText(source.Length, allocator);
                text.CopyFrom(source);
                symbols.Add(new TextSymbol(SymbolType.Text, text));
                parsedStringLength = source.Length;
                return;
            }

            symbols = new UnsafeList<TextSymbol>(16, allocator);
            parsedStringLength = 0;
            length = 0;

            Parse(ref source, allocator);
        }

        public UnsafeList<TextSymbol> symbols;
        public int parsedStringLength;
        public int length;

        [BurstCompile]
        void Parse(ref UnsafeText source, Allocator allocator)
        {
            var buffer = new UnsafeText(16, Allocator.Temp);
            var enumerator = new UnsafeTextEnumerator(source);

            var currentSymbol = SymbolType.Text;
            var prevRune = default(Unicode.Rune);

            parsedStringLength = 0;

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;

                if (current == '<' && currentSymbol is not (SymbolType.TagStart or SymbolType.TagEnd))
                {
                    if (buffer.Length > 0)
                    {
                        var text = new UnsafeText(buffer.Length, allocator);
                        text.CopyFrom(buffer);
                        symbols.Add(new TextSymbol(currentSymbol, text));
                        if (currentSymbol == SymbolType.Text) parsedStringLength += text.Length;
                        buffer.Clear();
                    }
                    buffer.Append(current);
                    currentSymbol = SymbolType.TagStart;
                }
                else if (current == '/' && prevRune == '<')
                {
                    buffer.Append(current);
                    currentSymbol = SymbolType.TagEnd;
                }
                else if (current == '>' && currentSymbol is SymbolType.TagStart or SymbolType.TagEnd)
                {
                    buffer.Append(current);
                    if (buffer.Length > 0)
                    {
                        var text = new UnsafeText(buffer.Length, allocator);
                        text.CopyFrom(buffer);
                        symbols.Add(new TextSymbol(currentSymbol, text));
                        if (currentSymbol == SymbolType.Text) parsedStringLength += text.Length;
                        buffer.Clear();
                    }
                    currentSymbol = SymbolType.Text;
                }
                else
                {
                    buffer.Append(current);
                }

                prevRune = current;
            }

            if (buffer.Length > 0)
            {
                var text = new UnsafeText(buffer.Length, allocator);
                text.CopyFrom(buffer);
                symbols.Add(new TextSymbol(currentSymbol, text));
                parsedStringLength += text.Length;
            }

            buffer.Dispose();
        }

        [BurstCompile]
        public unsafe void SliceParsedString(int from, int to, Allocator allocator, out UnsafeText text, out int resultLength)
        {
            text = new UnsafeText(length, allocator);

            var symbolsPtr = symbols.Ptr;
            var offset = 0;
            var tagIndent = 0;
            resultLength = 0;

            for (int i = 0; i < symbols.Length; i++)
            {
                var symbol = symbolsPtr + i;
                switch (symbol->type)
                {
                    case SymbolType.Text:
                        var enumerator = new UnsafeTextEnumerator(symbol->text);
                        while (enumerator.MoveNext())
                        {
                            var current = enumerator.Current;
                            if (from <= offset && offset <= to)
                            {
                                text.Append(current);
                                resultLength++;
                            }
                            offset++;

                            if (offset >= to && tagIndent == 0) goto LOOP_END;
                        }
                        break;
                    case SymbolType.TagStart:
                        text.Append(symbol->text);
                        tagIndent++;
                        break;
                    case SymbolType.TagEnd:
                        text.Append(symbol->text);
                        tagIndent--;
                        if (offset >= to && tagIndent == 0) goto LOOP_END;
                        break;
                }
            }

        LOOP_END:
            return;
        }

        public void Dispose()
        {
            if (!symbols.IsCreated) return;
            for (int i = 0; i < symbols.Length; i++)
            {
                symbols[i].Dispose();
            }
            symbols.Dispose();
        }

        public readonly struct TextSymbol : IDisposable
        {
            public TextSymbol(SymbolType type, UnsafeText text)
            {
                this.type = type;
                this.text = text;
            }

            public readonly SymbolType type;
            public readonly UnsafeText text;

            public void Dispose()
            {
                if (text.IsCreated) text.Dispose();
            }
        }

        public enum SymbolType : byte
        {
            Text,
            TagStart,
            TagEnd
        }
    }

    public ref struct UnsafeTextEnumerator
    {
        UnsafeText target;
        int offset;
        Unicode.Rune current;

        public UnsafeTextEnumerator(UnsafeText source)
        {
            target = source;
            offset = 0;
            current = default;
        }

        public bool MoveNext()
        {
            if (offset >= target.Length) return false;
            unsafe
            {
                Unicode.Utf8ToUcs(out current, target.GetUnsafePtr(), ref offset, target.Length);
            }
            return true;
        }

        public Unicode.Rune Current => current;
    }
}