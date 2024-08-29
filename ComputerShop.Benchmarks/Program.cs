using BenchmarkDotNet.Running;
using ComputerShop.Benchmarks;

#if RELEASE

var passwordStorageResult = BenchmarkRunner.Run<PasswordStorageBenchmark>();
var passwordValidationResult = BenchmarkRunner.Run<PasswordValidationBenchmark>();

#else

IHashAlgorithm
    plainText = new PlainTextNoHash(),
    sha1 = new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA1Name),
    md5 = new SimpleHashAlgorithm(SimpleHashAlgorithm.MD5Name),
    sha256 = new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA2_256Name),
    sha384 = new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA2_384Name),
    sha512 = new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA2_512Name),
    pbkdf2 = new PBKDF2HashAlgorithm(),
    argon2id = new Argon2idHashAlgorithm();

string passwordForBenchmark = "Pa$$w0rd@Bench0";

Console.WriteLine(passwordForBenchmark);
Console.WriteLine(sha1.CreateHashString(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(md5.CreateHashString(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(sha256.CreateHashString(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(sha384.CreateHashString(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(sha512.CreateHashString(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(pbkdf2.CreateHashString(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(argon2id.CreateHashString(Encoding.UTF8.GetBytes(passwordForBenchmark)));

var x = new PasswordValidationBenchmark();
Console.WriteLine(x.PlainText_Benchmark());
Console.WriteLine(x.MD5_Benchmark());
Console.WriteLine(x.SHA1_Benchmark());
Console.WriteLine(x.SHA2_256_Benchmark());
Console.WriteLine(x.SHA2_384_Benchmark());
Console.WriteLine(x.SHA2_512_Benchmark());
Console.WriteLine(x.PBKDF2_Benchmark());
Console.WriteLine(x.Argon2id_Benchmark());

#endif