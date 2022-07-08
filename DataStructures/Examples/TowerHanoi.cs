using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Examples
{
    public class TowerHanoi
    {
        public int DiscCount { get; private set; }

        public int MovesCount { get; private set; }

        public Stack<int> From { get; private set; }
        public Stack<int> To { get; private set; }

        public Stack<int> Auxiliary { get; private set; }

        public event EventHandler<EventArgs> MoveCompleted;

        public TowerHanoi(int discCount)
        {
            this.DiscCount = discCount;
            this.From = new Stack<int>();
            this.To = new Stack<int>();
            this.Auxiliary = new Stack<int>();

            //largest size block goes to bottom
            for (int i = discCount; i > 0; i--)
            {
                this.From.Push(i);
            }
        }

        public void Start()
        {
            
        }

        public void Move(int discs, Stack<int> from, Stack<int> to,
            Stack<int> auxiliary)
        {
            if (discs == 0)
                return;


        }
    }
}
