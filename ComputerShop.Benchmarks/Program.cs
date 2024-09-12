#if RELEASE
using BenchmarkDotNet.Running;
using ComputerShop.Benchmarks;

var passwordStorageResult = BenchmarkRunner.Run<PasswordStorageBenchmark>();
var passwordValidationResult = BenchmarkRunner.Run<PasswordValidationBenchmark>();
var encryptionValidationResult = BenchmarkRunner.Run<EncryptionBenchmark>();
var decryptionValidationResult = BenchmarkRunner.Run<DecryptionBenchmark>();
var tokenDigitalSignatureBenchmark = BenchmarkRunner.Run<TokenDigitalSignatureBenchmark>();
var addUserBenchmark = BenchmarkRunner.Run<AddUserBenchmark>();

#else
using ComputerShop.Server.Cryptography.Hash;
using System.Text;

IHashAlgorithm
    plainText = new PlainTextNoHash(),
    sha1 = new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA1Name),
    md5 = new SimpleHashAlgorithm(SimpleHashAlgorithm.MD5Name),
    sha256 = new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA2_256Name),
    sha384 = new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA2_384Name),
    sha512 = new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA2_512Name),
    pbkdf2 = new PBKDF2HashAlgorithm(),
    argon2id = new Argon2idHashAlgorithm();


string passwordForBenchmark = "X_5y>%6ZQ";

Console.WriteLine(passwordForBenchmark);
Console.WriteLine(md5.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(sha1.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(sha256.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(sha384.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(sha512.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(pbkdf2.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(argon2id.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine();

passwordForBenchmark = "Pa$$w0rds123!@#";

Console.WriteLine(passwordForBenchmark);
Console.WriteLine(md5.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(sha1.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(sha256.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(sha384.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(sha512.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(pbkdf2.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(argon2id.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine();

passwordForBenchmark = "admin1234";

Console.WriteLine(passwordForBenchmark);
Console.WriteLine(md5.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(sha1.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(sha256.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(sha384.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(sha512.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(pbkdf2.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(argon2id.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine();

passwordForBenchmark = "4>P6n%";

Console.WriteLine(passwordForBenchmark);
Console.WriteLine(md5.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(sha1.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(sha256.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(sha384.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(sha512.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(pbkdf2.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(argon2id.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));

#endif