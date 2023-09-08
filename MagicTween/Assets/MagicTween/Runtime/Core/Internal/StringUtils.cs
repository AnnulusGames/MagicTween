using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using Unity.Burst;

namespace MagicTween.Core
{
    [BurstCompile]
    internal unsafe static class StringUtils
    {
        static readonly char[] LowercaseChars = new char[]
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        };
        static readonly char[] UppercaseChars = new char[]
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        };
        static readonly char[] NumeralsChars = new char[]
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };
        static readonly char[] AllChars = new char[]
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };

        public static UnsafeText CreateTweenedText(ref UnsafeText start, ref UnsafeText end, float t, ScrambleMode scrambleMode, bool richTextEnabled, ref UnsafeText customChars, Allocator allocator)
        {
            var startParsed = new ParsedUnsafeText(start, richTextEnabled, Allocator.Temp);
            var endParsed = new ParsedUnsafeText(end, richTextEnabled, Allocator.Temp);

            FillText(ref startParsed, ref endParsed, t, scrambleMode, ref customChars, allocator, out var result);

            startParsed.Dispose();
            endParsed.Dispose();

            return result;
        }

        [BurstCompile]
        public unsafe static void FillText(
            ref ParsedUnsafeText start,
            ref ParsedUnsafeText end,
            float t,
            ScrambleMode scrambleMode,
            ref UnsafeText customChars,
            Allocator allocator,
            out UnsafeText result)
        {
            var length = math.max(start.parsedStringLength, end.parsedStringLength);
            var currentTextLength = (int)math.round(length * t);

            end.SliceParsedString(0, currentTextLength, Allocator.Temp, out var slicedText1, out var length1);
            start.SliceParsedString(currentTextLength + 1, length - 1, Allocator.Temp, out var slicedText2, out var length2);

            result = new UnsafeText(math.max(start.length, end.length), allocator);
            result.Append(slicedText1);
            result.Append(slicedText2);

            var customCharsLength = 0;
            if (customChars.IsCreated) customCharsLength = GetLengthOfUnsafeTextChars(ref customChars);

            for (int i = 0; i < length - (length1 + length2); i++)
            {
                AppendScrambleChar(ref result, scrambleMode, ref customChars, customCharsLength);
            }

            slicedText1.Dispose();
            slicedText2.Dispose();
        }

        [BurstCompile]
        public static int GetLengthOfUnsafeTextChars(ref UnsafeText text)
        {
            int length = 0;
            var enumerator = new UnsafeTextEnumerator(text);
            while (enumerator.MoveNext()) length++;
            return length;
        }

        [BurstCompile]
        public static void AppendScrambleChar(ref UnsafeText text, ScrambleMode scrambleMode, ref UnsafeText customChars, int customCharsLength)
        {
            switch (scrambleMode)
            {
                default: break;
                case ScrambleMode.Uppercase:
                    text.Append(UppercaseChars[SharedRandom.NextInt(0, UppercaseChars.Length)]);
                    break;
                case ScrambleMode.Lowercase:
                    text.Append(LowercaseChars[SharedRandom.NextInt(0, LowercaseChars.Length)]);
                    break;
                case ScrambleMode.Numerals:
                    text.Append(NumeralsChars[SharedRandom.NextInt(0, NumeralsChars.Length)]);
                    break;
                case ScrambleMode.All:
                    text.Append(AllChars[SharedRandom.NextInt(0, AllChars.Length)]);
                    break;
                case ScrambleMode.Custom:
                    text.Append(GetRuneOf(ref customChars, SharedRandom.NextInt(0, customCharsLength)));
                    break;
            }
        }

        [BurstCompile]
        public static Unicode.Rune GetRuneOf(ref UnsafeText text, int charIndex)
        {
            int index = 0;
            var enumerator = new UnsafeTextEnumerator(text);
            while (enumerator.MoveNext())
            {
                if (index == charIndex) return enumerator.Current;
                index++;
            }
            return Unicode.BadRune;
        }
    }
}