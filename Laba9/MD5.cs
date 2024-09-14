namespace MD5_Lib
{
    /// <summary>
    /// MD5
    /// </summary>
    public class MD5_h
    {
     /// <summary>
     /// Сдвиг влево
     /// </summary>
        static int[] s = new int[64] {
           /* 0..15*/7, 12, 17, 22,  7, 12, 17, 22,  7, 12, 17, 22,  7, 12, 17, 22,
            /* 16..31*/5,  9, 14, 20,  5,  9, 14, 20,  5,  9, 14, 20,  5,  9, 14, 20,
            /* 32..47*/4, 11, 16, 23,  4, 11, 16, 23,  4, 11, 16, 23,  4, 11, 16, 23,
            /* 48..63*/6, 10, 15, 21,  6, 10, 15, 21,  6, 10, 15, 21,  6, 10, 15, 21
        };

        /// <summary>
        /// Таблица констант по "2^32 × abs (sin(i + 1))"
        /// </summary>
        static uint[] T = new uint[64] {
            0xd76aa478, 0xe8c7b756, 0x242070db, 0xc1bdceee,
            0xf57c0faf, 0x4787c62a, 0xa8304613, 0xfd469501,
            0x698098d8, 0x8b44f7af, 0xffff5bb1, 0x895cd7be,
            0x6b901122, 0xfd987193, 0xa679438e, 0x49b40821,
            0xf61e2562, 0xc040b340, 0x265e5a51, 0xe9b6c7aa,
            0xd62f105d, 0x02441453, 0xd8a1e681, 0xe7d3fbc8,
            0x21e1cde6, 0xc33707d6, 0xf4d50d87, 0x455a14ed,
            0xa9e3e905, 0xfcefa3f8, 0x676f02d9, 0x8d2a4c8a,
            0xfffa3942, 0x8771f681, 0x6d9d6122, 0xfde5380c,
            0xa4beea44, 0x4bdecfa9, 0xf6bb4b60, 0xbebfbc70,
            0x289b7ec6, 0xeaa127fa, 0xd4ef3085, 0x04881d05,
            0xd9d4d039, 0xe6db99e5, 0x1fa27cf8, 0xc4ac5665,
            0xf4292244, 0x432aff97, 0xab9423a7, 0xfc93a039,
            0x655b59c3, 0x8f0ccc92, 0xffeff47d, 0x85845dd1,
            0x6fa87e4f, 0xfe2ce6e0, 0xa3014314, 0x4e0811a1,
            0xf7537e82, 0xbd3af235, 0x2ad7d2bb, 0xeb86d391
        };
        /// <summary>
        /// Циклический сдвиг влево
        /// </summary>
        /// <param name="x">Сдвигаемые данные</param>
        /// <param name="c">Таблица сдвига</param>
        /// <returns>Сдвинутые влево данные</returns>
        public static uint leftRotate(uint x, int c)
        {
            return (x << c) | (x >> (32 - c));
        }

        /// <summary>
        /// Хеш-функция MD5
        /// </summary>
        /// <param name="input">Входные данные</param>
        /// <returns>готовый хеш</returns>
        public string Calculate(byte[] input)
        {
            uint a0 = 0x67452301;   // A
            uint b0 = 0xefcdab89;   // B
            uint c0 = 0x98badcfe;   // C
            uint d0 = 0x10325476;   // D
            //дозаполняем до кратности 64-ем байтам(или 512 битам)
            var processedInputBuilder = new List<byte>(input) { 0x80 };//добавляем 1 к изначальному Input
            while (processedInputBuilder.Count % 64 != 56) processedInputBuilder.Add(0x0);//добавляем нули до заполнения  56 байтами(448 битами)
            processedInputBuilder.AddRange(BitConverter.GetBytes((long)input.Length * 8)); // заполняем оставшиеся 8 байтов(64 битами) символами длиной с изначальный инпут
            var processedInput = processedInputBuilder.ToArray();
            //Разбиваем подготовленное сообщение на 64-байтные (512 - битные) "куски"
            for (int i = 0; i < processedInput.Length / 64; ++i)
            {
                uint[] M = new uint[16];//массив для хранения input разделенного на 16 частей 4-байтовых(32-битных) слов
                for (int j = 0; j < 16; ++j)
                    M[j] = BitConverter.ToUInt32(processedInput, (i * 64) + (j * 4));

                // инициализация 4рёх слов для текущего куска
                uint A = a0, B = b0, C = c0, D = d0, F = 0, g = 0;

                // основные операции
                for (uint k = 0; k < 64; ++k)//проходимся пао каждому байту(биту)
                {
                    if (k <= 15)//1ый раунд
                    {
                        F = (B & C) | (~B & D);
                        g = k;//сдвиг по k
                    }
                    else if (k >= 16 && k <= 31)//второй раунд
                    {
                        F = (D & B) | (~D & C);
                        g = ((5 * k) + 1) % 16;
                    }
                    else if (k >= 32 && k <= 47)//третий раунд
                    {
                        F = B ^ C ^ D;
                        g = ((3 * k) + 5) % 16;
                    }
                    else if (k >= 48)//четвертый раунд
                    {
                        F = C ^ (B | ~D);
                        g = (7 * k) % 16;
                    }
                    //меняем местами
                    var dtemp = D;
                    F = (A + F + T[k] + M[g]);
                    D = C;
                    C = B;
                    B = B + leftRotate(F, s[k]);//получение F и битовый сдвиг
                    A = dtemp;
                }
                // Прибавляем результат текущего "куска" к общему результату
                a0 += A;
                b0 += B;
                c0 += C;
                d0 += D;
            }

            return GetByteString(a0) + GetByteString(b0) + GetByteString(c0) + GetByteString(d0);//результат
        }
        /// <summary>
        /// Перевод из байтов в строку
        /// </summary>
        /// <param name="x">Переводимая строка</param>
        /// <returns>Байты в формате строки</returns>
        private static string GetByteString(uint x)
        {
            return String.Join("", BitConverter.GetBytes(x).Select(y => y.ToString("x2")));
        }
    }
}