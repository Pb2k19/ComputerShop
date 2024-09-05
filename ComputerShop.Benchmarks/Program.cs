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
using ComputerShop.Benchmarks;
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

string passwordForBenchmark = "Pa$$w0rd@Bench0";

Console.WriteLine(passwordForBenchmark);
Console.WriteLine(sha1.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(md5.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(sha256.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(sha384.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(sha512.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(pbkdf2.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));
Console.WriteLine(argon2id.PasswordStorage(Encoding.UTF8.GetBytes(passwordForBenchmark)));

var x = new PasswordValidationBenchmark();
Console.WriteLine(x.PlainText_Benchmark());
Console.WriteLine(x.MD5_Benchmark());
Console.WriteLine(x.SHA1_Benchmark());
Console.WriteLine(x.SHA2_256_Benchmark());
Console.WriteLine(x.SHA2_384_Benchmark());
Console.WriteLine(x.SHA2_512_Benchmark());
Console.WriteLine(x.PBKDF2_Benchmark());
Console.WriteLine(x.Argon2id_Benchmark());

EncryptionBenchmark encryptionBenchmark = new();
DecryptionBenchmark decryptionBenchmark = new();

Console.WriteLine(encryptionBenchmark.AesEncryption_Benchmark());
Console.WriteLine(encryptionBenchmark.DesEncryption_Benchmark());
Console.WriteLine(encryptionBenchmark.RsaEncryption_Benchmark());

Console.WriteLine(Encoding.UTF8.GetString(decryptionBenchmark.AesDecryption_Benchmark()));
Console.WriteLine(Encoding.UTF8.GetString(decryptionBenchmark.DesDecryption_Benchmark()));
Console.WriteLine(Encoding.UTF8.GetString(decryptionBenchmark.RsaDecryption_Benchmark()));

AddUserBenchmark addUserBenchmark = new();
await addUserBenchmark.UserService_AesEncryption_Argon2idPassword();
#endif