using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using Unity.Entities;
using TMPro;

namespace MagicTween.Core.Systems
{
    [UpdateInGroup(typeof(MagicTweenTranslationSystemGroup))]
    public sealed partial class TMPTweenAnimatorUpdateSystem : SystemBase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TMPTweenAnimator GetAnimator(TMP_Text text)
        {
            if (!animatorMap.TryGetValue(text, out var animator))
            {
                animator = AddAnimator(text);
                animatorMap.Add(text, animator);
            }

            return animator;
        }

        TMPTweenAnimator AddAnimator(TMP_Text text)
        {
            var animator = new TMPTweenAnimator(text);

            if (tail == animators.Length)
            {
                Array.Resize(ref animators, tail * 2);
            }
            animators[tail] = animator;
            tail++;
            return animator;
        }

        readonly Dictionary<TMP_Text, TMPTweenAnimator> animatorMap = new();

        TMPTweenAnimator[] animators = new TMPTweenAnimator[8];
        int tail;

        protected override void OnUpdate()
        {
            var j = tail - 1;

            for (int i = 0; i < animators.Length; i++)
            {
                var animator = animators[i];
                if (animator != null)
                {
                    if (!animator.UpdateInternal())
                    {
                        animators[i] = null;
                    }
                    else
                    {
                        continue;
                    }
                }

                while (i < j)
                {
                    var fromTail = animators[j];
                    if (fromTail != null)
                    {
                        if (!fromTail.UpdateInternal())
                        {
                            animators[j] = null;
                            j--;
                            continue;
                        }
                        else
                        {
                            animators[i] = fromTail;
                            animators[j] = null;
                            j--;
                            goto NEXT_LOOP;
                        }
                    }
                    else
                    {
                        j--;
                    }
                }

                tail = i;
                break;

            NEXT_LOOP:
                continue;
            }
        }
    }
}