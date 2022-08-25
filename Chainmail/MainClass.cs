using System.Runtime.CompilerServices;

namespace Chainmail;
using Chainmail.Data;
using Chainmail.Model;
using System.Linq;
public class MainClass
{
    public static void Main(string[] args)
    {
        //Checking args
        if (args.Length <= 1)
        {
            Console.WriteLine(args.Length == 0 ? "Need both a source and dest dir" : "Need a destination dir");
            Environment.Exit(64);
        }
        //Check if the source file exists and the destination dir exists
        if (!File.Exists(args[0]) | !Directory.Exists(args[1]))
        {
            Console.WriteLine("Source file or destination dir does not exist");
            Environment.Exit(64);
        }

        var dbPath = args[0];
        var destDir = args[1];
        Console.WriteLine("Progress Log");
        Console.WriteLine("============");
        Console.WriteLine("Constructing all repositories");
        var attachmentRepo = new SQLiteAttachmentRepository(dbPath);
        var chatRepo = new SqLiteChatRepository(dbPath);
        var chatHandleJoinRepo = new SQLiteChatHandleJoinRepository(dbPath);
        var chatMesssageJoinRepo = new SQLiteChatMessageJoinRepository(dbPath);
        var deletedMessageRepo = new SQLiteDeletedMessageRepository(dbPath);
        var handleRepo = new SQLiteHandleRepository(dbPath);
        var messageRepo = new SQLiteMessageRepository(dbPath);
        var messageAttachmentJoinRepo = new SQLiteMessageAttachmentJoinRepository(dbPath);
        var syncDeletedAttachmentRepo = new SQLiteSyncDeletedAttachmentRepository(dbPath);
        var syncDeletedChatRepo = new SQLiteSyncDeletedChatRepository(dbPath);
        var syncDeletedMessageRepo = new SQLiteSyncDeletedMessageRepository(dbPath);
        Console.WriteLine("Beginning construction");
        try
        {
            Console.WriteLine("Getting chats");
            var chats = chatRepo.GetChats();
            Console.WriteLine("Getting chat handles");
            var chatHandleJoins = chatHandleJoinRepo.Get();
            Console.WriteLine("Getting chat messages");
            var chatMessageJoins = chatMesssageJoinRepo.Get();
            Console.WriteLine("Getting messages attachments");
            var messageAttachmentJoins = messageAttachmentJoinRepo.Get();
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message);
            Environment.Exit(64);
        }
        
        //Read data
        //Select Using Join Table
        //Join using message table =>
        //Export to destination using separate files for conversations
    }
}
