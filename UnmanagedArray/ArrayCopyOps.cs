﻿using UnmanagedArray.Allocators;
using System;

namespace UnmanagedArray
{
    public unsafe partial class Array<T>
    {
        public static implicit operator IntPtr(Array<T> e) => e.Buffer;

        public void Resize(long newLength)
        {
            var oldBuffer = Buffer;
            var oldLength = Length;

            if(oldLength == newLength)
            {
                return;
            }

            Buffer = default(IntPtr);
            Length = default(long);

            var newBuffer = Allocator.Allocate<T>(newLength, true);
            Allocator.Copy<T>(newBuffer, oldBuffer, oldLength);

            Buffer = newBuffer;
            Length = newLength;
        }

        public Array<T> Copy()
        {
            var e = new Array<T>(Length);
            Allocator.Copy<T>(e, this, Length);
            return e;
        }

        public void CopyFrom(T[] source)
        {
            Allocator.Copy(this, source);
        }

        public void CopyTo(T[] target)
        {
            var len = checked((int)Math.Min(Length, target.Length));
            Allocator.Copy(target, this);
        }        
    }
}