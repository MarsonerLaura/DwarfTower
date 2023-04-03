![DwarfTower-01](https://user-images.githubusercontent.com/104200268/229464486-eda42072-7438-4765-875d-38fe7483f765.png)
<p align="center"><i>DwarfTower</i> is a local multiplayer tower defense game. Players can collect coins to upgrade towers and place towers to defeat enemies. The game plays in a steampunk world and features dwarfs as main characters that try to free and defend their island.</p>

<br>

<div align="center">
 
`Unity`
`C#`
`UnityCollab`
`Rider`
`Krita`


</div>

---

<p>
<img align="left" width="50%" height="auto" src="https://user-images.githubusercontent.com/104200268/229472589-dbc11e4c-ef85-468f-9c81-d3d469900246.png">
 <br>
 <br>
<h1>About</h1>
<li><b>Role:</b>&emsp;&emsp;&emsp;&emsp;Programmer, Level Designer</li>
<li><b>Duration:</b>&emsp;&emsp;3 Weeks</li>
<li><b>Group Size:</b>&emsp;2</li>
<li><b>Engine:</b>&emsp;Unity</li>
<li><b>Genre:</b>&emsp;&emsp;&emsp;&nbsp;Local Multiplayer Tower Defense</li>
<li><b>Platform:</b>&emsp;&emsp;PC</li>
<li><b>Context:</b>&emsp;&emsp;&nbsp;Level Engineering Course</li>
</p>

<br>
<br>
<br>

<p>
<div>
<img align="right" width="47%" height="auto" src="https://user-images.githubusercontent.com/104200268/229356609-da4fde8a-7fe5-4a16-9e17-ee0c75489f78.jpg">
<br>
 <br>
<h1>Responsibilities</h1>
<li>Level Design</li>
<li>Wavemanager implementation</li>
<li>Enemies (Creation Tool, Animations)</li>
<li>Towers (Attacks, Animations, VFX)</li>
<li>Light baking & Post Processing</li>
<li>Prototyping</li>
<br>
</div>
</p>

<br>
 <br>
 
<p>
<div>
<img align="left" width="50%" height="auto" src="https://user-images.githubusercontent.com/104200268/229357404-977edd8f-7a90-4829-9a33-8aa3956f8cfb.jpg">
<br>
 <br>
 <br>
 <h1>Features</h1>
<li>Local Multiplayer</li>
<li>Place Different Towers</li>
<li>Collect Coins</li>
<li>Upgrade Towers</li>
<li>Defeat Various Enemies</li>
<li>Editor Tools</li>
</div>
</p>

<br>
<br>
<br>

---


 <a href="https://www.youtube.com/watch?v=0X8kur32egw&ab_channel=LukasPichler" target="_blank"><img src="https://user-images.githubusercontent.com/104200268/227638337-fd73fd4e-50a8-41b3-9bd4-4d418f4fe416.png" 
alt="Watch Trailer on YouTube" align="right" width="60%" height="auto" border="10" /></a>
<br>
 <br>
  <br>
<div align="center"> Klick on the Image on the right or the button below to watch the Trailer on YouTube! 
<br>
<br>

 
[![Watch Trailer on YouTube](https://img.shields.io/badge/Watch%20Trailer-FF0000?logo=youtube&style=for-the-badge)](https://www.youtube.com/watch?v=0X8kur32egw&ab_channel=LukasPichler) 

</div>

<br>
<br>


---

<p>
<h1>Additional Details</h1>

<!--
<h2>Code Snippets</h2>


<details>
 <summary>Leaderboard Code Snippets</summary>
 
 > <details> 
 >  <summary>Leaderboard Activity class that collects all users and displays them sortet by XP Points</summary>
 >
 > ```java
 > public class LeaderboardActivity extends AppCompatActivity {
 >     private RecyclerView mRecyclerView;
 >     private LeaderboardAdapter mRecyclerAdapter;
 >     List<SetViewItem> items = new ArrayList<>();
 >     ArrayList<User> users = new ArrayList<User>();
 >     String name = "",xp = "";
 >     private final Gson gson = new Gson();
 >
 >     //Sets the layout and displays the users sortet by XP
 >     @Override
 >     protected void onCreate(Bundle savedInstanceState) {
 >         super.onCreate(savedInstanceState);
 >         setContentView(R.layout.activity_leaderboard);
 >         mRecyclerView = (RecyclerView) findViewById(R.id.leaderboard_recyclerview);
 >         mRecyclerAdapter = new LeaderboardAdapter(users);
 >         final LinearLayoutManager layoutManager = new LinearLayoutManager(this);
 >         layoutManager.setOrientation(LinearLayoutManager.VERTICAL);
 >         mRecyclerView.setLayoutManager(layoutManager);
 >         mRecyclerView.setAdapter(mRecyclerAdapter);
 >
 >         fetchUsers();
 >         sortUsersByXp();
 >         mRecyclerAdapter.notifyData(users);
 >     }
 >
 >     //Fetches the userdata from the database
 >     private void fetchUsers() {
 >         HTTPGetter get = new HTTPGetter();
 >         get.execute("user", "getAll");
 >         try {
 >             String getUserResult = get.get();
 >             if (!getUserResult.equals("{ }")) {
 >                 User[]userArr= gson.fromJson(getUserResult, User[].class);
 >                 for (User user : userArr){
 >                     users.add(user);
 >                 }
 >             }
 >         } catch (ExecutionException e) {
 >             e.printStackTrace();
 >         } catch (InterruptedException e) {
 >             e.printStackTrace();
 >         }
 >     }
 >
 >     //Sorts the Users by XP points
 >     private void sortUsersByXp(){
 >         Collections.sort(users);
 >     }
 > }
 > ```
 > </details> 

</details>

 
 
<details>
 <summary>Presentation</summary>
 <br>
 
 >  <div align="center">
 >  To summarize the features and technologies used the final presentation is linked below:
 >  <br>
 >
 >  [Präsentation Social Gaming.pdf](https://github.com/MarsonerLaura/PuzzleHunt/files/11132678/Prasentation.Social.Gaming.pdf)
 > </div>
 > <br>
 
</details> 
-->

<h2>Game Design Process</h2>
<details>
 <summary>Story</summary>
 <br>
 
 >  <div align="center">
 >  The story of the game evolves around two dwarf friend engineers that visit their home island and discover that it was run over by an evil force that controlls undead. They start their adventure to collect mechanical parts to construct a big robot that should protect the island. On their jouney they need to defeat different enemies and help bewohners to get to the parts.
 >  <img width="80%" height="auto" src="https://user-images.githubusercontent.com/104200268/229501634-84a928f9-61c4-413d-9cd1-616d261749a8.png">
 > </div>
 > <br>
 
</details> 

<details>
 <summary>Gameplay</summary>
 <br>
 > <details> 
 >  <summary>Towers</summary>
 >  <div align="center">
 >  Electro Tower Before
 >  <img width="90%" height="auto" src="https://user-images.githubusercontent.com/104200268/229505972-6821954c-7049-4531-aa9f-0194f42ef641.gif">
 >  Electro Tower After
 >  <img width="90%" height="auto" src="https://user-images.githubusercontent.com/104200268/229516308-b7b7e6b4-65df-406a-a560-cf7fe25a60c1.gif">
 >  Speed Tower Before
 >  <img width="90%" height="auto" src="https://user-images.githubusercontent.com/104200268/229517032-fa907571-e919-455f-9ae0-9d2885f9b976.gif">
 >  Speed Tower After
 >  <img width="90%" height="auto" src="https://user-images.githubusercontent.com/104200268/229517170-927ce515-75e3-4a05-a1cb-adbf21cfc0f6.gif">
 >  </div>
 >  <br>
 > </details>
 
 > <details> 
 >  <summary>Player-Tower Interaction</summary>
 >  <div align="center">
 >  Players can pick up towers by pressing the respective key that shows up and also place them. As it is a local multiplayer game the key depends on the player.
 >  <img width="90%" height="auto" src="https://user-images.githubusercontent.com/104200268/229515511-1569be7d-8551-41d0-93f8-833a62f2ff9c.gif">
 >  </div>
 >  <br>
 > </details>

 > <details> 
 >  <summary>Collect Coins</summary>
 >  <div align="center">
 >  Players can collect coins by running through them. These coins can be used to buy upgrades for towers.
 >  <img width="90%" height="auto" src="https://user-images.githubusercontent.com/104200268/229505912-eed1e5df-42a1-4cf4-830d-ad9d248848bb.gif">
 >  </div>
 >  <br>
 > </details>

 > <details> 
 >  <summary>Enemies</summary>
 >  <div align="center">
 >  There exist different types of enemies with different attributes: The Base enemy with average health and speed compared to the oters, the Speedy enemy which is small and fast, the Tanky enemy which is slow and healthy and the Boss enemy which is very slow and tanky. 
 >  <img width="80%" height="auto" src="https://user-images.githubusercontent.com/104200268/229503757-9235d158-c451-4a0a-9e74-95f9fdb5c774.gif">
 >  </div>
 >  <br>
 > </details>

 > <details> 
 >  <summary>Wave Manager</summary>
 >  <div align="center">
 >  Is used to configure enemy wave spawning for the levels.
 >  <img width="70%" height="auto" src="https://user-images.githubusercontent.com/104200268/229503687-a79e35d8-4ac1-4047-9ebb-5c0f7ed9c445.png">
 >  </div>
 >  <br>
 > </details>

 > <details> 
 >  <summary>Tools</summary>
 >  <div align="center">
 >  Random Object Placement
 >  <img width="70%" height="auto" src="https://user-images.githubusercontent.com/104200268/229503592-1c498c77-49b8-434a-8c7a-d58390f9f26d.png">
 >  <br>
 >  Enemy Prefab Creation
 >  <img width="70%" height="auto" src="https://user-images.githubusercontent.com/104200268/229503608-927cfed7-3d73-4657-a142-e3d01d6c657a.png">
 >  </div>
 >  <br>
 > </details>
 
</details> 

<details>
 <summary>Level Design Process</summary>
 <br>
 
 >  <div align="center">
 >  Initial Prototype
 >  <img width="100%" height="auto" src="https://user-images.githubusercontent.com/104200268/229474718-86156057-936e-4639-b6be-1a7d6a493a5e.png">
 >  <br>
 >  Assets
 >  <br>
 >  <img width="100%" height="auto" src="https://user-images.githubusercontent.com/104200268/229474782-e6695b85-f3e6-4623-8396-6aead4a1f96c.png">
 >  <br>
 >  Light Baking & Post Processing
 >  <br>
 >  <img width="100%" height="auto" src="https://user-images.githubusercontent.com/104200268/229475909-ef819f0a-e923-4740-a429-5398f32dfae7.png">
 >  <br>
 >  Details and VFX
 >  <br>
 >  <img width="100%" height="auto" src="https://user-images.githubusercontent.com/104200268/229474831-79b7d189-cb4f-40b7-974a-66b3c3f88579.png">
 >  <br>
 >  Level Design Decisions
 >  <br>
 >  <img width="100%" height="auto" src="https://user-images.githubusercontent.com/104200268/229474892-b776e17d-a615-4828-b3a2-87927df9e119.png">
 >  <br>
 >  <img width="100%" height="auto" src="https://user-images.githubusercontent.com/104200268/229474863-518e66ce-d1a3-4353-bbc2-cc07bb774243.png">
 >  <br>
 >  <img width="100%" height="auto" src="https://user-images.githubusercontent.com/104200268/229475961-e0ff54cf-c4d2-4bed-8c6c-75bfaab2eed1.png">
 > </div>
 > <br>
 
</details> 

</p>
