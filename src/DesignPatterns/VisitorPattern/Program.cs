using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace VisitorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Visitor Pattern!");

            Form form = Get();


            IVisitor visitor = new HtmlVisitor();

            form.Accept(visitor);

            string html = visitor.Output;

            System.IO.File.WriteAllText("index.html", html);
        }

        public static Form Get()
        {
            Form form = new Form
            {
                Name = "/forms/customers",
                Title = "Design Patterns",

                Body = new Collection<Control>
                {

                    new LabelControl { Caption = "Person", Name = "lblName" },
                    new TextBoxControl { Caption = "FirstName", Name = "txtFirstName", Value = "John"},
                    new CheckBoxControl { Caption = "IsAdult", Name = "chkIsAdult", Value = true },
                    new ButtonControl {  Caption = "Submit", Name = "btnSubmit", ImageSource = "save.png" },
                }

            };

            return form;
        }
    }

    #region Models

    public class Form
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public ICollection<Control> Body { get; set; }

        public void Accept(IVisitor visitor)
        {
            foreach (var control in Body)
            {
                control.Accept(visitor);
            }
        }

       
    }

    // Abstract Visitor
    public interface IVisitor
    {
        void Visit(LabelControl control);
        void Visit(TextBoxControl control);
        void Visit(CheckBoxControl control);
        void Visit(ButtonControl control);
        string Output { get; }
    }

    // Concrete Visitor
    public class HtmlVisitor : IVisitor
    {
        private StringBuilder builder = new StringBuilder();

        public HtmlVisitor()
        {
            builder.AppendLine("<html>");
        }

        public void Visit(LabelControl control)
        {
            builder.AppendLine($"<span>{control.Caption}</span>");
        }

        public void Visit(TextBoxControl control)
        {
            builder.AppendLine($"<span>{control.Caption}</span><input type='text' value={control.Value}></input>");
        }

        public void Visit(CheckBoxControl control)
        {
            builder.AppendLine($"<span>{control.Caption}</span><input type='checkbox' value={control.Value}></input>");
        }

        public void Visit(ButtonControl control)
        {
            builder.AppendLine($"<button><img src='{control.ImageSource}'></img>{control.Caption}</button>");
        }

        public string Output
        {
            get
            {
                builder.AppendLine("</html>");

                return builder.ToString();
            }
        }
    }


    public class XmlVisitor : IVisitor
    {
        public string Output => throw new NotImplementedException();

        public void Visit(LabelControl control)
        {
            throw new NotImplementedException();
        }

        public void Visit(TextBoxControl control)
        {
            throw new NotImplementedException();
        }

        public void Visit(CheckBoxControl control)
        {
            throw new NotImplementedException();
        }

        public void Visit(ButtonControl control)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class Control
    {
        public string Name { get; set; }
        public string Caption { get; set; }

        public abstract void Accept(IVisitor visitor);
    }

    public class LabelControl : Control
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class TextBoxControl : Control
    {
        public string Value { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class CheckBoxControl : Control
    {
        public bool Value { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class ButtonControl : Control
    {
        public string ImageSource { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }


    //public enum ControlType
    //{
    //    Label,
    //    TextBox,
    //    Checkbox,
    //    Button
    //}




    #endregion

}
