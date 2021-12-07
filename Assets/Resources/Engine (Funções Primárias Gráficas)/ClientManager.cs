using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Colyseus;
using System.Threading.Tasks;
using RSG;
using Object = UnityEngine.Object;

public class ClientManager : MonoBehaviour
{
    #region Variaveís de Ambiente (Estáticas)

    static Colyseus.ColyseusClient Client;
    static public GameObject ClientManagerObj;

    static public ColyseusRoom<State> GameRoom;
    static public CameraScript GameCamera;
    static public GameObject SafeArea;

    static public bool Loaded = false;

    // Pra identificar o usuário e seu personagem de escolha.
    static public PlayerSchema User;
    static public Character Character;
    static public Component SkillManager;

    static public GameObject preFabSelectTarget;
    static public GameObject SelectTargetScreen;

    static string RoomId;
    static string Token;

    static public Object CharacterCard;
    static Text Latency;
    public static float Latency_;

    static public Dictionary<string, Object> Entities;
    static public Dictionary<string, Object> Elements;
    static public Dictionary<string, Object> Animations;
    static public Object EntityBarPrefab;
    static public Object PopupPrefab;

    #endregion

    #region Funções de Inicialização de Entidades e Gerenciamento de Pacotes Prontos
    // Pega todas entidades de um personagem.
    static Dictionary<string, Object> GetEntitiesFromChar(string Char)
    {
        switch (Char)
        {
            case "Aegis":
                return AegisEntityPackage.GetEntities();
        }

        return null;
    }
    static Dictionary<string, Object> GetElementsFromChar(string Char)
    {
        switch (Char)
        {
            case "Aegis":
                return AegisEntityPackage.GetElements();
        }

        return null;
    }
    static Dictionary<string, Object> GetAnimationsFromChar(string Char)
    {
        switch (Char)
        {
            case "Aegis":
                return AegisEntityPackage.GetAnimations();
        }

        return null;
    }

    static void InterfaceFunctionFromChar(string Char)
    {
        switch (Char)
        {
            case "Aegis":
                AegisEntityPackage.CreateInterface();
                break;
        }

        return;
    }
    #endregion

    #region Classes de Tipo de Mensagem

    class IdentifyAssignment
    {
        public string id;
    }

    class CheckActionInfo
    {
        public string type;
        public int index;

        // if Type == A
        public float YR;
        public float GR;
        public float BR;
        public float speed;

        public int targetType;
        public string[] teamsSup;
        public float circleRadius;
        public float boxWidth;
        public float boxHeight;
        public int maxOrder;
    }

    class AnimInfo
    {
        // Anim_ = animação
        // Popup = popup
        public string name;
        public string pType;
        public string pValue;
        public int[] color_ = {255, 255, 255, 255};

        public float x;
        public float y;
    }

    #endregion

    #region Carregar e Descarregar Tela de Carregamento de Personagens
    // Carrega a tela de carregamento.
    static void LoadAwaitScreen()
    {
        GameObject TeamA = GameObject.Find("TeamA_Load");
        GameObject TeamB = GameObject.Find("TeamB_Load");
        CharacterCard = Resources.Load("UIComponents/CharacterCard/CharacterCard") as GameObject;

        GameRoom.State.player.ForEach((key, player) =>
        {
            GameObject Team;
            GameObject card;
            CharacterCardScript cardScript;

            switch (GameRoom.State.player.Count)
            {
                case 2:
                    if (player.team == "A") Team = TeamA;
                    else Team = TeamB;
                    card = GameObject.Instantiate(CharacterCard, new Vector3(Team.transform.position.x, Team.transform.position.y, Team.transform.position.z), Quaternion.identity) as GameObject;
                    card.transform.SetParent(Team.transform);

                    cardScript = card.GetComponent<CharacterCardScript>();
                    cardScript.UserId = player.id;
                    cardScript.Render();
                    break;

                case 4:
                    if (player.team == "A") Team = TeamA;
                    else Team = TeamB;
                    card = GameObject.Instantiate(CharacterCard, new Vector3(Team.transform.position.x, Team.transform.position.y, Team.transform.position.z), Quaternion.identity) as GameObject;
                    card.transform.SetParent(Team.transform);

                    cardScript = card.GetComponent<CharacterCardScript>();
                    cardScript.UserId = player.id;
                    cardScript.Render();

                    if (Team.transform.childCount == 1)
                    {
                        card.transform.position += new Vector3(170f, 0f, 0f);
                    } else
                    {
                        card.transform.position -= new Vector3(170f, 0f, 0f);
                    }
                    break;

                case 6:
                    if (player.team == "A") Team = TeamA;
                    else Team = TeamB;
                    card = GameObject.Instantiate(CharacterCard, new Vector3(Team.transform.position.x, Team.transform.position.y, Team.transform.position.z), Quaternion.identity) as GameObject;
                    card.transform.SetParent(Team.transform);

                    cardScript = card.GetComponent<CharacterCardScript>();
                    cardScript.UserId = player.id;
                    cardScript.Render();

                    if (Team.transform.childCount == 1)
                    {
                        card.transform.position += new Vector3(300f, 0f, 0f);
                    }
                    else if(Team.transform.childCount == 2)
                    {
                        card.transform.position -= new Vector3(300f, 0f, 0f);
                    } 
                    break;
            }
        });
    }

    // Finaliza a tela de carregamento.
    static void EndAwaitScreen()
    {
        GameObject LoadScreen = GameObject.Find("LoadScreen");
        Destroy(LoadScreen);
    }
    #endregion

    #region Entidades e Elementos (Sincronização)

    // Inicializa as entidades e sincroniza a existência delas com o servidor.
    static void EntitySync()
    {
        EntityBarPrefab = Resources.Load("Entities/EntityLifeBar");
        SafeArea = GameObject.Find("Safe Area");

        Entities = new Dictionary<string, Object>();
        GameRoom.State.player.ForEach((key, player) => {
            Character playerChar = GameRoom.State.entities[player.characterAttached];
            Dictionary<string, Object> CharPackage = GetEntitiesFromChar(playerChar.id);
            foreach (KeyValuePair<string, Object> Entity in CharPackage)
            {
                try
                {
                    Entities.Add(Entity.Key, Entity.Value);
                }
                catch { }
            }
        });

        GameRoom.State.entities.ForEach((entityId, character) => {
            GameObject x = Instantiate(Entities[character.id], new Vector3(character.vector.x, character.vector.y, character.vector.y * 2 + SafeArea.transform.position.z), Quaternion.identity) as GameObject;
            x.transform.SetParent(SafeArea.transform);
            x.name = entityId;
            x.transform.localScale = new Vector3(character.root.scaleX, character.root.scaleY, 0);

            EntityScript con = x.GetComponent<EntityScript>();
            con.entityId = entityId;
            con.Start_();
            if (GameRoom.State.loading == false) con.StartAnim();
        });

        GameRoom.State.entities.OnAdd += (string entityId, Character character) =>
        {
            GameObject x = Instantiate(Entities[character.id], new Vector3(character.vector.x, character.vector.y, character.vector.y * 2 + SafeArea.transform.position.z), Quaternion.identity) as GameObject;
            x.transform.SetParent(SafeArea.transform);
            x.name = entityId;
            x.transform.localScale = new Vector3(character.root.scaleX, character.root.scaleY, 0);

            EntityScript con = x.GetComponent<EntityScript>();
            con.entityId = entityId;
            con.Start_();
            con.StartAnim();
        };

        GameRoom.State.entities.OnRemove += (string entityId, Character character) =>
        {
            GameObject x = GameObject.Find(entityId);
            Destroy(x);
        };
    }

    // Inicializa os elementos e sincroniza a existência deles com o servidor.
    static void ElementSync()
    {
        Elements = new Dictionary<string, Object>();
        GameRoom.State.player.ForEach((key, player) => {
            Character playerChar = GameRoom.State.entities[player.characterAttached];
            Dictionary<string, Object> CharPackage = GetElementsFromChar(playerChar.id);
            foreach (KeyValuePair<string, Object> Element in CharPackage)
            {
                try
                {
                    Elements.Add(Element.Key, Element.Value);
                }
                catch { }
            }
        });

        GameRoom.State.elements.ForEach((elementId, element) => {
            GameObject x = Instantiate(Elements[element.id], new Vector3(element.vector.x, element.vector.y, element.vector.y * 2 + SafeArea.transform.position.z), Quaternion.identity) as GameObject;
            x.transform.SetParent(SafeArea.transform);
            x.name = elementId;
            x.transform.localScale = new Vector3(element.root.scaleX, element.root.scaleY, 0);

            ElementScript con = x.GetComponent<ElementScript>();
            con.elementId = elementId;
            con.Start_();
        });

        GameRoom.State.elements.OnAdd += (string elementId, Element_ element) =>
        {
            GameObject x = Instantiate(Elements[element.id], new Vector3(element.vector.x, element.vector.y, element.vector.y * 2 + SafeArea.transform.position.z), Quaternion.identity) as GameObject;
            x.transform.SetParent(SafeArea.transform);
            x.name = elementId;
            x.transform.localScale = new Vector3(element.root.scaleX, element.root.scaleY, 0);

            ElementScript con = x.GetComponent<ElementScript>();
            con.elementId = elementId;
            con.Start_();
        };

        GameRoom.State.elements.OnRemove += (string elementId, Element_ element) =>
        {
            GameObject x = GameObject.Find(elementId);
            x.SendMessage("ComeDestroy");
        };
    }

    // Sincroniza a existência das animações espontâneas.
    static void AnimationSync()
    {
        PopupPrefab = Resources.Load("UIComponents/GameDinamic/Popup/Popup") as GameObject;
        GameObject EntitiesCanvas = GameObject.Find("EntitiesCanvas");

        Animations = new Dictionary<string, Object>();
        GameRoom.State.player.ForEach((key, player) => {
            Character playerChar = GameRoom.State.entities[player.characterAttached];
            Dictionary<string, Object> CharPackage = GetAnimationsFromChar(playerChar.id);
            foreach (KeyValuePair<string, Object> Animation in CharPackage)
            {
                try
                {
                    Animations.Add(Animation.Key, Animation.Value);
                }
                catch { }
            }
        });

        GameRoom.OnMessage<AnimInfo>("Animate", (anim) =>
        {
            if(anim.name == "Popup")
            {
                GameObject p = Instantiate(PopupPrefab, new Vector3(anim.x, anim.y, anim.y * 2 + SafeArea.transform.position.z), Quaternion.identity) as GameObject;
                p.transform.parent = EntitiesCanvas.transform;
                PopupScript m = p.GetComponent<PopupScript>();
                m.Start_(anim.pValue, anim.pType, anim.color_);
                return;
            }

            GameObject a = Instantiate(Animations[anim.name], new Vector3(anim.x, anim.y, -30 + SafeArea.transform.position.z), Quaternion.identity) as GameObject;
        });
    }
    #endregion

    #region Gerenciamento de Habilidades e Minigames de Precisão

    // Inicializa as operações de CheckAction (Minigames para conclusão de habilidades).
    static void CheckActionSync()
    {
        GameObject VerifyCanvas = GameObject.Find("VerifyCanvas");
        GameObject BarPrecisionVerify = Resources.Load("UIComponents/VerifyPrefabs/BarPrecisionVerify/BarPrecisionVerify") as GameObject;

        void useAction(CheckActionInfo serverInfo, string[] targets, string stringInfo = "R")
        {
            GameRoom.Send("RemoveSkillsBlock", "");

            UseActionClass m = new UseActionClass();
            m.setHabIndex(serverInfo.index);
            m.setParams(stringInfo, targets);
            GameRoom.Send("UseAction", m);
        }

        void CheckAction_Bar(Func<string, string> callback, Func<string, string> errorCallback, CheckActionInfo serverInfo)
        {
            if (serverInfo.type != "A") return;
            if (VerifyCanvas.transform.childCount != 0) return;

            GameObject x = Instantiate(BarPrecisionVerify, new Vector3(0f, -100f, 0f), Quaternion.identity);
            x.transform.SetParent(VerifyCanvas.transform);
            x.transform.localPosition = new Vector3(0, -100);
            BarPrecisionVerifyScript manager = x.GetComponent<BarPrecisionVerifyScript>();

            manager.Initialize(serverInfo.YR, serverInfo.GR, serverInfo.BR, serverInfo.speed).Then((string_) =>
            {
                // Executa o callback com a LETRA da cor SELECIONADA.
                callback(string_);
            }).Catch((err) => { errorCallback(""); });
        }

        GameRoom.OnMessage<CheckActionInfo>("CheckPhase", (message) =>
        {
            // Verifica se é pra selecionar alvo.
            if(message.targetType != -1)
            {
                GameRoom.Send("RequestSkillsBlock", "");
                GetTargets(message).Then((targets) =>
                {
                    // Verifica o tipo de minigame.
                    switch (message.type)
                    {
                        // COM MINIGAMES (LETRA MAISCULA)
                        case "A":
                            CheckAction_Bar((stringInfo) =>
                            {
                                useAction(message, targets, stringInfo);
                                return "";
                            }, (error) => { GameRoom.Send("RemoveSkillsBlock", ""); return ""; }, message);
                            break;
                        // SEM MINIGAMES (off)
                        case "off":
                            useAction(message, targets, "R");
                            break;
                    }
                }).Catch((err) => { GameRoom.Send("RemoveSkillsBlock", ""); });
            } else
            {
                GameRoom.Send("RequestSkillsBlock", "");
                // Verifica o tipo de minigame.
                switch (message.type)
                {
                    // COM MINIGAMES (LETRA MAISCULA)
                    case "A":
                        CheckAction_Bar((stringInfo) =>
                        {
                            useAction(message, new string[1], stringInfo);
                            return "";
                        }, (error) => { GameRoom.Send("RemoveSkillsBlock", ""); return ""; }, message);
                        break;
                    // SEM MINIGAMES (off)
                    case "off":
                        useAction(message, new string[1], "R");
                        break;
                }
            }
        });
    }
    
    // Função que retorna os alvos selecionados.
    static IPromise<string[]> GetTargets(CheckActionInfo Info)
    {
        Promise<string[]> result = new Promise<string[]>();
        if (SelectTargetScreen)
        {
            result.Reject(new System.Exception());
            return result;
        }
        
        GameObject SafeArea = GameObject.Find("Safe Area");
        SelectTargetScreen = Instantiate(preFabSelectTarget, Vector3.zero, Quaternion.identity);
        SelectTargetScreen.transform.SetParent(SafeArea.transform);
        SelectTargetScreen.transform.localPosition = new Vector3(0, 0, -75);

        SelectTargetScript x = SelectTargetScreen.GetComponent<SelectTargetScript>();
        x.Initialize(Info.targetType, Info.teamsSup, Info.circleRadius, Info.boxWidth, Info.boxHeight, Info.maxOrder).Then((array_) =>
        {
            result.Resolve(array_);
        }).Catch((err) =>
        {
            result.Reject(new System.Exception());
        });

        return result;
    }

    #endregion

    #region Conexão e Inicialização

    // Inicializa a conexão.
    static async void Connect()
    {
        try
        {
            Client = new Colyseus.ColyseusClient("ws://lycaria.herokuapp.com");
            string[] info = System.Environment.GetCommandLineArgs();

            //RoomId = info[1]; 
            //Token = info[2];

            RoomId = "yQjZbPdGx";
            //Cliente A
            Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJfaWQiOiI1ZWQ5YzhmYzFjYTY0MTE4MmM5MjIxNzIiLCJlbWFpbCI6ImxpdHRsZXR3b2d6QGhvdG1haWwuY29tIiwicGFzcyI6IiQyYSQxMCQyb2xNYXFCRzRwZC9VN3MxRkRNck11ZXNOZ0tBUHZxV01Bblp4UTNDakdmMzV1alFaYWlSLiIsImlhdCI6MTYzODg2OTIzNCwiZXhwIjoxNjQxNDYxMjM0fQ.nZbYbDcmu-R-6nkyvUcCpBzwHS_GNvangXdd6idpMe0";
            //Cliente B
            //Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJfaWQiOiI1ZWQ5YzhmYzFjYTY0MTE4MmM5MjIxNzIiLCJlbWFpbCI6ImxpdHRsZXR3b2d6QGhvdG1haWwuY29tIiwicGFzcyI6IiQyYSQxMCQyb2xNYXFCRzRwZC9VN3MxRkRNck11ZXNOZ0tBUHZxV01Bblp4UTNDakdmMzV1alFaYWlSLiIsImlhdCI6MTYzNjg3MzcwMywiZXhwIjoxNjM5NDY1NzAzfQ.duNJSpcOEukR4D-V-ipYxFZfzEY-0DZ9TPZECxE04cM";

            Dictionary<string, object> options = new Dictionary<string, object>();

            options.Add("token", Token);

            GameRoom = await Client.JoinById<State>(RoomId, options);

            GameRoom.OnMessage<IdentifyAssignment>("IdentifyAssignment", async(message) =>
            {
                User = GameRoom.State.player[message.id];
                Character = GameRoom.State.entities[User.characterAttached];
                Loaded = true;

                ClientManagerObj.AddComponent(Type.GetType(Character.id+"SkillManager"));
                SkillManager = ClientManagerObj.GetComponent(Type.GetType(Character.id + "SkillManager"));
                
                LoadAwaitScreen();
                EntitySync();
                ElementSync();
                AnimationSync();
                CheckActionSync();
                InterfaceFunctionFromChar(Character.id);

                await Task.Delay(7000);
                await GameRoom.Send("ConfirmLoad", "");

            });

            GameRoom.OnMessage<bool>("CloseLoadScreen", async (matchIsRunning) =>
            {             
                // Se a partida não estiver acontecendo ainda, todas as entidades farão animação de entrada.
                if(!matchIsRunning)
                {
                    EndAwaitScreen();
                    GameRoom.State.entities.ForEach((entityId, character) => {
                        GameObject x = GameObject.Find(entityId);
                        x.GetComponent<EntityScript>().StartAnim();
                    });
                } else
                {
                    GameRoom.State.entities.ForEach((entityId, character) => {
                        GameObject x = GameObject.Find(entityId);
                        x.GetComponent<EntityScript>().StartAnim();
                    });
                    await Task.Delay(10000);
                    EndAwaitScreen();
                }

                GameCamera.zoomIn(() => {
                    GameCamera.goToCamera(0, -5, 1);
                    return 0; 
                });

            });

            await GameRoom.Send("RequestIdentify", "");

            Latency = GameObject.Find("Latency").GetComponent<Text>();
            GameRoom.OnMessage<float>("pong", (ms) =>
            {
                var lat = (Time.time * 1000 - ms) / 2;
                Latency_ = lat;
                Latency.text = Mathf.RoundToInt(lat).ToString() + " ms";

                if(lat < 75)
                {
                    Latency.color = new Color32(168, 255, 163, 255);
                } else if (lat < 100)
                {
                    Latency.color = new Color32(255, 237, 134, 255);
                } else
                {
                    Latency.color = new Color32(253, 163, 156, 255);
                }
            });

            PingPong();
        }
        catch
        {
            Application.Quit();
        }
    }

    // Checa o PING.
    static async void PingPong()
    {
        await GameRoom.Send("ping", Time.time*1000);
        await Task.Delay(4000);
        PingPong();
    }
    
    void Start()
    {
        preFabSelectTarget = Resources.Load("UIComponents/SelectTarget/SelectTargetScreen") as GameObject;
        GameCamera = GameObject.Find("Main Camera").GetComponent<CameraScript>();
        ClientManagerObj = GameObject.Find("ClientManager");
        Connect();
    }

    #endregion
}
