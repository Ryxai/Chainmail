using System.Runtime.CompilerServices;
using Chainmail.Model.SQL;
using Chainmail.Transformer;

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
        Console.WriteLine("");
        Console.WriteLine("Retrieving contents of database");
        Console.WriteLine("============");
        IEnumerable<Attachment> attachments = Array.Empty<Attachment>();
        IEnumerable<Chat> chats = Array.Empty<Chat>();
        IEnumerable<ChatHandleJoin> chatHandleJoins = Array.Empty<ChatHandleJoin>();
        IEnumerable<ChatMessageJoin> chatMessageJoins = Array.Empty<ChatMessageJoin>();
        IEnumerable<DeletedMessage> deletedMessages = Array.Empty<DeletedMessage>();
        IEnumerable<Handle> handles = Array.Empty<Handle>();
        IEnumerable<Message> messages = Array.Empty<Message>();
        IEnumerable<MessageAttachmentJoin> messageAttachmentJoins = Array.Empty<MessageAttachmentJoin>();
        IEnumerable<SyncDeletedAttachment> syncDeletedAttachments = Array.Empty<SyncDeletedAttachment>();
        IEnumerable<SyncDeletedChat> syncDeletedChats = Array.Empty<SyncDeletedChat>();
        IEnumerable<SyncDeletedMessage> syncDeletedMessages = Array.Empty<SyncDeletedMessage>();
        try
        {
            Console.WriteLine("Getting attachments");
            attachments = attachmentRepo.GetAttachments();
            Console.WriteLine("Getting chats");
            chats = chatRepo.GetChats();
            Console.WriteLine("Getting chat handles");
            chatHandleJoins = chatHandleJoinRepo.Get();
            Console.WriteLine("Getting chat messages");
            chatMessageJoins = chatMesssageJoinRepo.Get();
            Console.WriteLine("Getting deleted messages");
            deletedMessages = deletedMessageRepo.GetDeletedMessages();
            Console.WriteLine("Getting handles");
            handles = handleRepo.GetHandles();
            Console.WriteLine("Getting messages");
            messages = messageRepo.GetMessages();
            Console.WriteLine("Getting messages attachments");
            messageAttachmentJoins = messageAttachmentJoinRepo.Get();
            Console.WriteLine("Getting sync deleted attachments");
            syncDeletedAttachments = syncDeletedAttachmentRepo.GetSyncDeletedAttachments();
            Console.WriteLine("Getting sync deleted chats");
            syncDeletedChats = syncDeletedChatRepo.GetSyncDeletedChats();
            Console.WriteLine("Getting sync deleted messages");
            syncDeletedMessages = syncDeletedMessageRepo.GetSyncDeletedMessages();
            Console.Write("Retrieved contents");
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine("Error: " + e.Message);
            Console.WriteLine("Terminating");
            Environment.Exit(64);
        }
        Console.WriteLine("Loading data");
        var coreDatabaseFactory = new CoreDatabaseFactory(messageAttachmentJoins.ToList(),
            chatHandleJoins.ToList(),
            chatMessageJoins.ToList());
        coreDatabaseFactory.Load(chats);
        coreDatabaseFactory.Load(syncDeletedChats);
        coreDatabaseFactory.Load(handles);
        coreDatabaseFactory.Load(attachments);
        coreDatabaseFactory.Load(syncDeletedAttachments);
        coreDatabaseFactory.Load(messages);
        coreDatabaseFactory.Load(deletedMessages);
        coreDatabaseFactory.Load(syncDeletedMessages);
        Console.WriteLine("Assembling data");
        var results = coreDatabaseFactory.Assemble().AsEnumerable().ToList();
        if (results.Length() == 0)
        {
            Console.WriteLine("No results");
            Environment.Exit(64);
        }

        var data = results[0];
        Console.WriteLine("Exporting data");
        if (args.Contains("--json"))
        {
            using var writer = new StreamWriter(Path.Combine(destDir, "data.json"));
            writer.Write(data.SerializeAsJson());
        }
        else
        {
            var chatThreads = data.ChatThreads.Zip(data.Serialize());
            foreach (var (first, threadData) in chatThreads)
            {
                var threadName = first.DisplayName;
                var threadPath = Path.Combine(destDir, threadName);
                Directory.CreateDirectory(threadPath);
                using var writer = new StreamWriter(Path.Combine(threadPath, "data.json"));
                writer.Write(threadData);
            }
        }
    }
}
