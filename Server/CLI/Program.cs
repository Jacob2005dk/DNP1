// See https://aka.ms/new-console-template for more information
using CLI.UI;
using DNP1;
using FileRepositories;

Console.WriteLine("Starting...");
IUserRepository userRepository = new UserFileRepository();
ICommentRepository commentRepository = new CommentFileRepository();
IPostRepository postRepository = new PostFileRepository();

CliApp cliApp = new CliApp(userRepository, commentRepository, postRepository);
await cliApp.StartAsync();

