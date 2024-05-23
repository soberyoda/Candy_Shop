using System.Security.Cryptography;
using System.Text;

namespace Candy_Shop.Utilities; 

public static class Crypto {
  public static string ToMd5(string input) {
    using MD5 md5 = MD5.Create();
    byte[] inputBytes = Encoding.UTF8.GetBytes(input);
    byte[] hashBytes = md5.ComputeHash(inputBytes);

    return Convert.ToHexString(hashBytes);
  }

  public static string SeededGuid(int seed) {
    var random = new Random(seed);
    byte[] guid = new byte[16];
    random.NextBytes(guid);
    
    return new Guid(guid).ToString();
  }
}
