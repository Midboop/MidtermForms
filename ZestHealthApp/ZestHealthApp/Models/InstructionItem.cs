using System;
using System.Collections.Generic;
using System.Text;

namespace ZestHealthApp.Models
{
    public class InstructionItem
    {
        public int Step { get; set; }
        public string Directive { get; set; }
        public InstructionItem()
        {
            Step = 0;
            Directive = string.Empty;
        }
        public InstructionItem(InstructionItem item)
        {
            Step = item.Step;
            Directive = item.Directive;
        }
        public InstructionItem(int step, string instructions)
        {
            Step = step;
            Directive = instructions;
        }
    }
}
