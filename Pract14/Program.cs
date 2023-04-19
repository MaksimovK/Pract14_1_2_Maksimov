using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Pract14
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            void TaskOne()
            {
                var stack = new Stack();
                uint n;

                while (true)
                {
                    Console.Write("Введите кол-во элементов: ");
                    if (uint.TryParse(Console.ReadLine(), out n))
                        break;
                }

                for (int i = 1; i <= n; i++)
                {
                    stack.Push(i);
                }

                Console.Write($"Размерость стека {stack.Count}\n" +
                              $"Верхний элемент стека {stack.Peek()}\n" +
                              $"Размерость стека {stack.Count}\n" +
                              $"Содержимое стека: ");
                foreach (var el in stack)
                    Console.Write(el + " ");
                stack.Clear();
                Console.WriteLine($"\nНовая размерность стека: {stack.Count}");
            }

            void TaskTwo()
            {
                Stack stack = new Stack();
                Console.Write("Введите выражение: ");
                string text = Console.ReadLine();
                int balance = 0;
                bool balanced = true;
                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == '(')
                    {
                        balance++;
                    }
                    else if (text[i] == ')')
                    {
                        balance--;
                    }

                    if (balance < 0)
                    {
                        balanced = false;
                        break;
                    }
                }

                if (balanced)
                {
                    Console.WriteLine("Скобки сбалансированы.");
                    stack.Push(text);
                    using (StreamWriter sr = new StreamWriter("Text.txt"))
                    {
                        sr.WriteLine(text);
                        sr.Close();
                    }
                }
                else
                {
                    Console.WriteLine("Возможно лишняя ( скобка в позиции: " + (text.LastIndexOf('(') + 1) + " )");
                }

                string newExpression = text;
                while (balance > 0)
                {
                    newExpression += ")";
                    balance--;
                }

                while (balance < 0)
                {
                    int index = newExpression.LastIndexOf('(');
                    newExpression = newExpression.Remove(index, 1);
                    balance++;
                }
                stack.Push(text);
                using (StreamWriter sr = new StreamWriter("Text1.txt"))
                {
                    sr.WriteLine(text);
                    sr.Close();
                }
            }

            Action action = new Action(TaskOne);
            action += TaskTwo;
            action?.Invoke();
        }
    }
}