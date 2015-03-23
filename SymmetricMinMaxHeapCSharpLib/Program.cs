using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymmetricMinMaxHeapCSharpLib
{
    public class SymmetricMinMaxHeap
    {
        private Int32 _Size;
        private Int32 _ArraySize;

        private Int32 _InitialSize;
        
        private List<UInt64> _L;


        public SymmetricMinMaxHeap(Int32 InitialSize = 4)
        {
            this._Size = 2;

            if (InitialSize < 4)
                this._ArraySize = 4;
            else
                this._ArraySize = InitialSize;

            this._L = new List<UInt64>(this._ArraySize);
            this._L.AddRange(Enumerable.Repeat<UInt64>(UInt64.MinValue, this._ArraySize));

            this._InitialSize = InitialSize;
        }

        ///////////////////
        // Inner Methods //
        ///////////////////

        private static Int32 uLnode(Int32 index)
        {
            Int32 temp = (index / 4) * 2;
            if (temp == 0)
                return 0;
            else
                return temp;
        }
        private static Int32 uRnode(Int32 index)
        {
            Int32 temp = (index / 4) * 2;
            if (temp == 0)
                return 0;
            else
                return temp + 1;
        }
        private static Int32 llLnode(Int32 index)
        {
            return index * 2;
        }
        private static Int32 llRnode(Int32 index)
        {
            return (index * 2) + 2;
        }
        private static Int32 lrLnode(Int32 index)
        {
            return (index * 2) - 1;
        }
        private static Int32 lrRnode(Int32 index)
        {
            return (index * 2) + 1;
        }
        private void Swap(Int32 indexA, Int32 indexB)
        {
            UInt64 temp = this._L[indexA];
            this._L[indexA] = this._L[indexB];
            this._L[indexB] = temp;
        }
        private void Resize(Int32 size)
        {
            Int32 CurrentSize = this._L.Count;
            if (size < CurrentSize)
                this._L.RemoveRange(size, CurrentSize - size);
            else if (size > CurrentSize)
            {
                if (size > this._L.Capacity)
                    this._L.Capacity = size;
                this._L.AddRange(Enumerable.Repeat<UInt64>(UInt64.MinValue, size - CurrentSize));
            }
        }
        private void Expand()
        {
            Int32 _TargetArraySize = this._ArraySize * 2;
            this.Resize(_TargetArraySize);
            this._ArraySize = _TargetArraySize;
        }
        

        public void Insert(UInt64 value)
        {
            this._L[this._Size] = value;
            Int32 _CurrentIndex = this._Size;
            this._Size++;
            if ((_CurrentIndex % 2) == 1)
            {
                if (this._L[_CurrentIndex - 1] > this._L[_CurrentIndex])
                {
                    this.Swap(_CurrentIndex - 1, _CurrentIndex);
                    _CurrentIndex--;
                }
            }

            Int32 _lnode = 0, _rnode = 0;
            while(true)
            {
                _lnode = SymmetricMinMaxHeap.uLnode(_CurrentIndex);
                _rnode = SymmetricMinMaxHeap.uRnode(_CurrentIndex);
                if ((_lnode != 0) && (this._L[_CurrentIndex] < this._L[_lnode]))
                {
                    this.Swap(_CurrentIndex, _lnode);
                    _CurrentIndex = _lnode;
                }
                else if ((_rnode != 0) && (this._L[_CurrentIndex] > this._L[_rnode]))
                {
                    this.Swap(_CurrentIndex, _rnode);
                    _CurrentIndex = _rnode;
                }
                else
                {
                    break;
                }
            }

            if (this._Size >= this._ArraySize)
                this.Expand();
        }

        private UInt64 TakeMin()
        {
            if (this._L[2] <= this._L[3])
                return this._L[2];
            else
                return this._L[3];
        }
        public UInt64 Min 
        {
            get { return TakeMin(); } 
        }

        private UInt64 TakeMax()
        {
            if (this._L[2] > this._L[3])
                return this._L[2];
            else
                return this._L[3];
        }
        public UInt64 Max
        {
            get { return TakeMax(); }
        }

        public void DeleteMin()
        {
            this._Size--;
            this._L[2] = this._L[this._Size];
            int _CurrentIndex = 2;

            Int32 _lnode = 0, _rnode = 0;
            while (true)
            {
                _lnode = _CurrentIndex * 2;//SymmetricMinMaxHeap.llLnode(_CurrentIndex);
                _rnode = (_CurrentIndex * 2) + 2;//SymmetricMinMaxHeap.llRnode(_CurrentIndex);

                if (_rnode < this._Size)
                {
                    if (this._L[_lnode] < this._L[_rnode])
                    {
                        if (this._L[_lnode] < this._L[_CurrentIndex])
                        {
                            if (this._L[_lnode] > this._L[_lnode + 1])
                            {
                                this.Swap(_lnode + 1, _CurrentIndex);
                                _CurrentIndex = (_lnode + 1);
                            }
                            else
                            {
                                this.Swap(_lnode, _CurrentIndex);
                                _CurrentIndex = _lnode;
                            }
                        }
                        else
                            break;

                    }
                    else if (this._L[_lnode] >= this._L[_rnode])
                    {
                        if (this._L[_rnode] < this._L[_CurrentIndex])
                        {

                            if ((_rnode + 1) < this._Size)
                            {
                                if (this._L[_rnode] > this._L[_rnode + 1])
                                {
                                    this.Swap(_rnode + 1, _CurrentIndex);
                                    _CurrentIndex = (_rnode + 1);
                                    continue;
                                }
                            }

                            this.Swap(_rnode, _CurrentIndex);
                            _CurrentIndex = _rnode;
                        }
                        else
                            break;
                    }
                }
                else if (_lnode < this._Size)
                {
                    if (this._L[_lnode] < this._L[_CurrentIndex])
                    {
                        if ((_lnode + 1) < this._Size)
                        {
                            if (this._L[_lnode] > this._L[_lnode + 1])
                            {
                                this.Swap(_lnode + 1, _CurrentIndex);
                                _CurrentIndex = (_lnode + 1);
                                continue;
                            }
                        }

                        this.Swap(_lnode, _CurrentIndex);
                        _CurrentIndex = _lnode;
                    }
                    else
                        break;
                }
                else
                {
                    if ((_CurrentIndex + 1) < this._Size)
                    {
                        if (this._L[_CurrentIndex] > this._L[_CurrentIndex + 1])
                            this.Swap(_CurrentIndex, _CurrentIndex + 1);
                    }
                    break;
                }
            }
        }

        public void DeleteMax()
        {
            this._Size--;
            this._L[3] = this._L[this._Size];
            int _CurrentIndex = 3;

            Int32 _lnode = 0, _rnode = 0;
            while (true)
            {
                _lnode = (_CurrentIndex * 2) - 1;//SymmetricMinMaxHeap.lrLnode(_CurrentIndex);
                _rnode = (_CurrentIndex * 2) + 1;//SymmetricMinMaxHeap.lrRnode(_CurrentIndex);

                //if ((_lnode < this._Size) && (_rnode < this._Size))
                if (_rnode < this._Size)
                {
                    if (this._L[_lnode] > this._L[_rnode])
                    {
                        if (this._L[_lnode] > this._L[_CurrentIndex])
                        {
                            if (this._L[_lnode] < this._L[_lnode + 1])
                            {
                                this.Swap(_lnode + 1, _CurrentIndex);
                                _CurrentIndex = (_lnode + 1);
                            }
                            else
                            {
                                this.Swap(_lnode, _CurrentIndex);
                                _CurrentIndex = _lnode;
                            }
                        }
                        else
                            break;

                    }
                    else if (this._L[_lnode] <= this._L[_rnode])
                    {
                        if (this._L[_rnode] > this._L[_CurrentIndex])
                        {

                            if ((_rnode + 1) < this._Size)
                            {
                                if (this._L[_rnode] < this._L[_rnode + 1])
                                {
                                    this.Swap(_rnode + 1, _CurrentIndex);
                                    _CurrentIndex = (_rnode + 1);
                                    continue;
                                }
                            }

                            this.Swap(_rnode, _CurrentIndex);
                            _CurrentIndex = _rnode;
                        }
                        else
                            break;
                    }
                }
                else if (_lnode < this._Size)
                {
                    if (this._L[_lnode] > this._L[_CurrentIndex])
                    {
                        if ((_lnode + 1) < this._Size)
                        {
                            if (this._L[_lnode] < this._L[_lnode + 1])
                            {
                                this.Swap(_lnode + 1, _CurrentIndex);
                                _CurrentIndex = (_lnode + 1);
                                continue;
                            }
                        }

                        this.Swap(_lnode, _CurrentIndex);
                        _CurrentIndex = _lnode;
                    }
                    else
                        break;
                }

                //else if (_rnode < this._Size)
                //{
                //    if (this._L[_rnode] > this._L[_CurrentIndex])
                //    {
                //        this.Swap(_rnode, _CurrentIndex);
                //        _CurrentIndex = _rnode;
                //    }
                //    else
                //        break;
                //}
                else
                {
                    if ((_CurrentIndex + 1) < this._Size)
                    {
                        if (this._L[_CurrentIndex] < this._L[_CurrentIndex + 1])
                            this.Swap(_CurrentIndex, _CurrentIndex + 1);
                    }
                    break;
                }
            }
        }

        public void Clear()
        {
            this._Size = 2;
            this._ArraySize = this._InitialSize;
            this.Resize(this._ArraySize);
        }

        private Int32 TakeSize()
        {
            return this._Size - 2;
        }
        public Int32 Size
        {
            get { return TakeSize(); }
        }

        public List<UInt64> Array
        {
            get { return this._L; }
        }

    }
}
