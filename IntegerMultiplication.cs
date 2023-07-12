using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Problem
{
    // ***
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // ***
    public static class IntegerMultiplication
    {


        //Your Code is Here:
        //==================
        /// <summary>
        /// Multiply 2 large integers of N digits in an efficient way [Karatsuba's Method]
        /// </summary>
        /// <param name="X">First large integer of N digits [0: least significant digit, N-1: most signif. dig.]</param>
        /// <param name="Y">Second large integer of N digits [0: least significant digit, N-1: most signif. dig.]</param>
        /// <param name="N">Number of digits (power of 2)</param>
        /// <returns>Resulting large integer of 2xN digits (left padded with 0's if necessarily) [0: least signif., 2xN-1: most signif.]</returns>


        public static byte[] Add(byte[] num1, byte[] num2)
        {

            byte[] add = new byte[num1.Length];

            byte carry = 0;

            if (num1.Length > num2.Length)
            {
                Array.Resize(ref num2, num2.Length + (num1.Length - num2.Length));
            }

            for (int i = 0; i < num1.Length; i++)
            {
                int sum = (byte)(num1[i] + num2[i] + carry);
                add[i] = (byte)((sum % 10));
                carry = (byte)(sum / 10);
            }
            if (carry != 0)
            {
                Array.Resize(ref add, num1.Length + 1);
                add[num1.Length] = (byte)(carry);
            }

            return add;
        }

        public static byte[] sub(byte[] num1, byte[] num2)
        {

            if (num1.Length > num2.Length)
            {
                Array.Resize(ref num2, num2.Length + (num1.Length - num2.Length));
            }
            else if (num1.Length < num2.Length)
            {
                Array.Resize(ref num1, num1.Length + (num2.Length - num1.Length));
            }

            byte[] sub = new byte[num1.Length];
            byte borrow = 0;
            int subt = 0;
            for (int i = 0; i < num1.Length; i++)
            {
                subt = num1[i] - num2[i] - borrow;
                if (subt < 0)
                {
                    subt += 10;
                    borrow = 1;

                }
                else
                {
                    borrow = 0;

                }
                sub[i] = (byte)subt;
            }

            return sub;
        }
        /*    public static byte[] combine(byte[] num1, int N)
            {
                int ln_num = num1.Length;
                byte[] new_num1 = new byte[ln_num + N];
                Array.Copy(num1, 0, new_num1, N, ln_num);
                return new_num1;
            }*/

        public static byte[] Combine(byte[] num1, byte[] num2, byte[] num3, int N)
        {
            int ln_num1 = num1.Length;
            int ln_num2 = num2.Length;
            byte[] new_num1 = new byte[ln_num1 + N];
            byte[] new_num2 = new byte[ln_num2 + N / 2];
            Array.Copy(num1, 0, new_num1, N, ln_num1);
            Array.Copy(num2, 0, new_num2, N / 2, ln_num2);
            byte[] result = Add(Add(new_num1, new_num2), num3);
            return result;
        }


        static public byte[] IntegerMultiply(byte[] X, byte[] Y, int N)
        {
            //REMOVE THIS LINE BEFORE START CODING
            // throw new NotImplementedException();
            //byte[] result = new byte[2 * N];
            N = Math.Max(X.Length, Y.Length);
            byte[] arr_base = new byte[2];

            // base case
            if (N == 1)
            {
                int sum = X[0] * Y[0];
                arr_base[1] = (byte)(sum / 10);
                arr_base[0] = (byte)(sum % 10);
                return arr_base;
            }

            if (N % 2 == 1)
            {
                N += 1;
            }
            // Divided
            byte[] A = X.Take(N / 2).ToArray();
            byte[] B = X.Skip(N / 2).ToArray();
            byte[] C = Y.Take(N / 2).ToArray();
            byte[] D = Y.Skip(N / 2).ToArray();

            if (A.Length > B.Length)
            {
                Array.Resize(ref B, B.Length + (A.Length - B.Length));
            }
            if (A.Length < B.Length)
            {
                Array.Resize(ref A, A.Length + (B.Length - A.Length));
            }
            if (C.Length > D.Length)
            {
                Array.Resize(ref D, D.Length + (C.Length - D.Length));
            }
            if (C.Length < D.Length)
            {
                Array.Resize(ref C, C.Length + (D.Length - C.Length));
            }

            // conquer
            byte[] AB = Add(A, B);
            byte[] CD = Add(C, D);

            byte[] AC = IntegerMultiply(A, C, N / 2);
            byte[] BD = IntegerMultiply(B, D, N / 2);
            byte[] multi = IntegerMultiply(AB, CD, CD.Length);
            byte[] sub_multiAC = sub(multi, BD);
            byte[] total_sub = sub(sub_multiAC, AC);

            // combine
            /*  byte[] res_BD = combine(BD, N);
              byte[] res_total_sub = combine(total_sub, N / 2);
              byte[] result = Add(res_BD, res_total_sub);
              byte[] final_result = Add(result, AC);*/
            byte[] final_result = Combine(BD, total_sub, AC, N);


            return final_result;

        }


    }
}