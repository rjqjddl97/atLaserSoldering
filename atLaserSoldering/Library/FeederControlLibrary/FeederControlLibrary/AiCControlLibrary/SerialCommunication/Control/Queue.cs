using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiCControlLibrary.SerialCommunication.Control
{
    public class Queue
    {
        private const int QueueSize = 10240;
        private byte[] m_pBuffer;
        private UInt32 m_nHead = 0;
        private UInt32 m_nFilled = 0;
        private UInt32 m_nMaxBufflen = 0;
        
        public Queue(UInt16 Size)
        {
            m_nMaxBufflen = Size;
            m_pBuffer = new byte[QueueSize];
            System.Array.Clear(m_pBuffer, 0, QueueSize);
            m_nHead = 0;
            m_nFilled = 0;
        }
        public bool IsEmpty()
        {
            if (m_nFilled == 0)
                return true;
            else
                return false;
        }
        public bool IsFull()
        {
            if (m_nFilled == m_nMaxBufflen)
                return true;
            else
                return false;
        }
        public void Flush()
        {
            //System.Array.Clear(m_pBuffer, 0, QueueSize);
            m_nHead = 0;
            m_nFilled = 0;
        }
        public UInt32 GetFilledSize()
        {
            return m_nFilled;
        }
        public UInt32 GetSpaceSize()
        {
            return m_nMaxBufflen - m_nFilled;
        }
        public bool Push(byte element)
        {
            uint Buffcnt = 0;
            if (IsFull())
            {
                return false;
            }
            Buffcnt = (m_nHead + m_nFilled) % QueueSize;
            m_pBuffer[Buffcnt] = element;
            m_pBuffer[Buffcnt + 1] = 0;
            m_nFilled++;
            return true;
        }
        public bool Peek(ref byte element)
        {
            if (IsEmpty())
                return false;
            element = m_pBuffer[m_nHead];
            return true;
        }
        public bool Pop(ref byte element)
        {            
            if (IsEmpty())
                return false;
            element = m_pBuffer[m_nHead];
            m_pBuffer[m_nHead] = 0;
            m_nHead++;
            m_nHead %= QueueSize; 
            m_nFilled--;
            return true;
        }
    }
}
