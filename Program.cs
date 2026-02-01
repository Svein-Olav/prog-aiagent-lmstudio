using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OpenAI;
using OpenAI.Chat ;
using System.ClientModel;

var lmStudioEndpoint = new Uri("http://localhost:1234/v1");

// VIKTIG: må matche /v1/models -> id
var model = "qwen2.5-7b-instruct";

// LM Studio krever ofte ikke nøkkel, men SDK-en trenger en placeholder
var credential = new ApiKeyCredential("lm-studio");

var openAi = new OpenAIClient(credential, new OpenAIClientOptions
{
    Endpoint = lmStudioEndpoint
});

await Oppgave1(model, openAi);

static async Task Oppgave1(string model, OpenAIClient openAi)
{
    OpenAI.Chat.ChatClient rawChatClient = openAi.GetChatClient(model);
    IChatClient chatClient = rawChatClient.AsIChatClient();

    AIAgent agent = new ChatClientAgent(
        chatClient,
        instructions: "Du er en hjelpsom assistent. Svar kort og vennlig.",
        name: "HelloAgent"
    );

    var response = await agent.RunAsync("Si hei, og fortell én morsom fakta om Norge.");
    Console.WriteLine(response.Text);
}


