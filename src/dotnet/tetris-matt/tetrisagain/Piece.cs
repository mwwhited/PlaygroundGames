using System;
using System.Collections.Generic;
using System.Text;

namespace tetrisagain
{
    public class Piece
    {
        public static readonly ushort I = 0x8888;
        public static readonly ushort J = 0x44C0;
        public static readonly ushort L = 0x88C0;
        public static readonly ushort O = 0xCC00;
        public static readonly ushort S = 0x8C40;
        public static readonly ushort T = 0xE400;
        public static readonly ushort Z = 0x4C80;

        private static Random _rand = new Random((int)DateTime.Now.Ticks);

        public static ushort RandomPiece()
        {
            switch (_rand.Next(0, 7))
            {
                case 0:
                    return I;
                case 1:
                    return J;
                case 2:
                    return L;
                case 3:
                    return O;
                case 4:
                    return S;
                case 5:
                    return T;
                case 6:
                default:
                    return Z;
            }
        }

        public static ushort RotateLeft(ushort piece)
        {
            piece = (ushort)(
                ((piece & 0x8000) >> 03) |
                ((piece & 0x4000) >> 06) |
                ((piece & 0x2000) >> 09) |
                ((piece & 0x1000) >> 12) |

                ((piece & 0x0800) << 02) |
                ((piece & 0x0400) >> 01) |
                ((piece & 0x0200) >> 04) |
                ((piece & 0x0100) >> 07) |

                ((piece & 0x0080) << 07) |
                ((piece & 0x0040) << 04) |
                ((piece & 0x0020) << 01) |
                ((piece & 0x0010) >> 02) |

                ((piece & 0x0008) << 12) |
                ((piece & 0x0004) << 09) |
                ((piece & 0x0002) << 06) |
                ((piece & 0x0001) << 03)
            );
            piece = ShakeRight(piece);
            piece = ShakeUp(piece);
            return piece;
        }

        public static ushort RotateRight(ushort piece)
        {
            piece = (ushort)(
                ((piece & 0x8000) >> 12) |
                ((piece & 0x4000) >> 07) |
                ((piece & 0x2000) >> 02) |
                ((piece & 0x1000) << 03) |

                ((piece & 0x0800) >> 09) |
                ((piece & 0x0400) >> 04) |
                ((piece & 0x0200) << 01) |
                ((piece & 0x0100) << 06) |

                ((piece & 0x0080) >> 06) |
                ((piece & 0x0040) >> 01) |
                ((piece & 0x0020) << 04) |
                ((piece & 0x0010) << 09) |

                ((piece & 0x0008) >> 03) |
                ((piece & 0x0004) << 02) |
                ((piece & 0x0002) << 07) |
                ((piece & 0x0001) << 12)
            );
            piece = ShakeRight(piece);
            piece = ShakeUp(piece);
            return piece;
        }

        private static ushort ShakeRight(ushort piece)
        {
            while ((piece & 0x8888) == 0)
                piece = (ushort)(piece <<1);
            return piece;
        }

        private static ushort ShakeUp(ushort piece)
        {
            while ((piece & 0xF000) == 0)
                piece = (ushort)(piece << 4);
            return piece;
        }

        public static int GetHeight(ushort piece)
        {
            if ((piece & 0xFFFF) == 0)
                return 0;
            else if ((piece & 0x0FFF) == 0)
                return 1;
            else if ((piece & 0x00FF) == 0)
                return 2;
            else if ((piece & 0x000F) == 0)
                return 3;
            else
                return 4;
        }

        public static int GetWidth(ushort piece)
        {
            if ((piece & 0xFFFF) == 0)
                return 0;
            else if ((piece & 0x7777) == 0)
                return 1;
            else if ((piece & 0x3333) == 0)
                return 2;
            else if ((piece & 0x1111) == 0)
                return 3;
            else
                return 4;
        }

        private static bool CanDown(ushort piece)
        {
            return false;
        }
    }
}
