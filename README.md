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
 >  To summarize the features and technologies used the final presentation is linked below:
 > 
 > </div>
 > <br>
 
</details> 

<details>
 <summary>Gameplay</summary>
 <br>
 
 >  <div align="center">
 >  To summarize the features and technologies used the final presentation is linked below:
 >  <br>
 >
 > </div>
 > <br>
 
</details> 
<details>
 <summary>Level Design Process</summary>
 <br>
 
 >  <div align="center">
 >  
 >  <br>
 >  <img width="90%" height="auto" src="https://user-images.githubusercontent.com/104200268/229474718-86156057-936e-4639-b6be-1a7d6a493a5e.png">
 >  <img width="90%" height="auto" src="https://user-images.githubusercontent.com/104200268/229474782-e6695b85-f3e6-4623-8396-6aead4a1f96c.png">
 >  <img width="90%" height="auto" src="https://user-images.githubusercontent.com/104200268/229474831-79b7d189-cb4f-40b7-974a-66b3c3f88579.png">
 >  <img width="90%" height="auto" src="https://user-images.githubusercontent.com/104200268/229474863-518e66ce-d1a3-4353-bbc2-cc07bb774243.png">
 >  <img width="90%" height="auto" src="https://user-images.githubusercontent.com/104200268/229474892-b776e17d-a615-4828-b3a2-87927df9e119.png">
 >  <img width="90%" height="auto" src="">
 >  <img width="90%" height="auto" src="">
 > </div>
 > <br>
 
</details> 

</p>
