using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour {

	#region Local variables
	private bool gameOver;
	public GameObject chimEnemy;
	public GameObject splitEnemy;
	public GameObject kronosEnemy;
	public GameObject butchEnemy;
	public GameObject chim;
	public GameObject split;
	public GameObject kronos;
	public GameObject butch;
	public Texture[] xHairTextures;
	public GameObject[] charBullets;
	public int playerToInit;
	public float startTime;
	public float gracePeriodDuration;
	public float matchDuration;
	public bool startCountDown = false;
	public bool doInit;
	public bool initHealthUI;
	public bool initReloadUI;
	public bool initClockUI;
	public bool initEHealthUI;
	public bool initWWEUI;
	public bool disableShooting;
	private GameObject activeCharacter;
	private GameObject activeEnemy;
	private GameObject healthBar;
	private GameObject reloadBar;
	private GameObject enemyHBar;
	private GameObject playerClock;
	private Text clockText;
	private GameObject damageDisplay;
	private GameObject bulletType;
	private GameObject xHair;
	private GameObject bullet;
	private CharacterParameters scriptOfActiveChar; //All hard-coded values are accessible here
	private CharacterParameters scriptOfActiveEnemy;
	private CrosshairScript scriptOfXHair;
	private bulletHole scriptOfPlayerHitBox;
	private bulletHole scriptOfAIHitbox;
	private int activeCharId; // IMPORTANT: Does not match with character select id
	private int activeEnemyId;
	private bool allowInitUI;
	private string myNameInHierachy;
	public bool isReloading;
	public bool startTimer;
	public int timeLeft;
	public NewNetworkManagerScript networkManager;
	#endregion

	//AK disable this Awake() when you're connecting photon to initPlayer() method.
	//ps: be wary of the active/nonactive state of hierachy characters
	public void Awake(){
		//if (doInit) {
		//	initPlayer (playerToInit, "player");
		//}
	}

	public void Start(){
		chim.SetActive(false);
		split.SetActive(false);
		kronos.SetActive(false);
		butch.SetActive(false);
		chimEnemy.SetActive(false);
		splitEnemy.SetActive(false);
		kronosEnemy.SetActive(false);
		butchEnemy.SetActive(false);
		isReloading = false;
		startTimer = false;

		gracePeriodDuration = 5f;
		matchDuration = 30f; //change to 30 later

		gameOver = false;
		startCountDown = false;


		networkManager = GameObject.Find ("NewNetworkManager").GetComponent<NewNetworkManagerScript> ();
		int playerCharacterId = networkManager.getPlayerCharacterId ();
		initPlayer (playerCharacterId, "player");
		initPlayer (UnityEngine.Random.Range (1, 4), "enemy");

		//DontDestroyOnLoad (this.gameObject);
		//initPClock ();
		//Invoke (changeClockText (playerClock, matchDuration, true), gracePeriodDuration);
	}

	public void Update() {
		//gracePeriodDuration -= Time.deltaTime;
		/*if (startCountDown) {
			changeClockText (playerClock, matchDuration, 0, false);
			matchDuration -= Time.deltaTime;
			if (matchDuration <= 0.0f) {
				endMatch ();
			}
		}*/

		/*if (startTimer) {
			timeLeft -= (int) Time.deltaTime;
			if (timeLeft < 0) {
			} else {
				string searchString = myNameInHierachy+"/PlayerUI/Timer";
				GameObject.Find (searchString).gameObject.GetComponentInChildren<Text>().text = "";
				float newWidth = (320f/30f)*((float)timeLeft);
				getHealthBar ().gameObject.GetComponentInChildren<RectTransform> ().sizeDelta = new Vector2 (newWidth, 40f);
			}
		}*/


		if (startCountDown && !gameOver) {
			changeClockText (playerClock, matchDuration, 0, false);
			matchDuration -= Time.deltaTime;
			if (matchDuration <= 0.0f) {
				endMatch ();
			}

		}

		if (Time.time > startTime + gracePeriodDuration && !gameOver) {
			startCountDown = true;
		}
			
		//CHANGE ACTIVE ENEMY POSITION HERE (AND SHOOT SOMETIMES YES AND SOMETIMES NO)


	}

	#region Getters and Setters
	//Active character model
	public GameObject getActiveCharacter(){
		return activeCharacter;
	}

	public void setActiveCharacter(GameObject _activeCharacter){
		activeCharacter = _activeCharacter;
	}

	//For active character script
	public CharacterParameters getActiveCharacterScript(){
		return scriptOfActiveChar;
	}

	public void setActiveCharacterScript(CharacterParameters _scriptOfActiveChar){
		scriptOfActiveChar = _scriptOfActiveChar;
	}

	public GameObject getActiveEnemy(){
		return activeEnemy;
	}

	public void setActiveEnemy(GameObject _activeEnemy){
		activeEnemy = _activeEnemy;
	}

	//For active character script
	public CharacterParameters getActiveEnemyScript(){
		return scriptOfActiveEnemy;
	}

	public void setActiveEnemyScript(CharacterParameters _scriptOfActiveEnemy){
		scriptOfActiveEnemy = _scriptOfActiveEnemy;
	}

	//For xHair script
	public CrosshairScript getActiveXHairScript(){
		return scriptOfXHair;
	}

	public void setActiveXHairScript(CrosshairScript _scriptOfXHair){
		scriptOfXHair = _scriptOfXHair;
	}

	// Collision script reference for player
	public bulletHole getPlayerHitboxScript(){
		return scriptOfPlayerHitBox;
	}

	public void setPlayerHitboxScript(bulletHole _scriptOfPlayerHitBox){
		scriptOfPlayerHitBox = _scriptOfPlayerHitBox;
	}

	// Collision script reference for enemy
	public bulletHole getEnemyHitboxScript(){
		return scriptOfAIHitbox;
	}

	public void setEnemyHitboxScript(bulletHole _scriptOfAIHitBox){
		scriptOfAIHitbox = _scriptOfAIHitBox;

	}

	//For active player number
	public int getActiveCharId(){
		return activeCharId;
	}

	public void setActiveCharId(int _activeCharId){
		activeCharId = _activeCharId;
	}

	//For active enemy number
	public int getEnemyId(){
		return activeEnemyId;
	}

	public void setEnemyId(int _activeEnemyId){
		activeEnemyId = _activeEnemyId;
	}

	//Health bar object
	public GameObject getHealthBar(){
		return healthBar;
	}

	public void setHealthBar(GameObject _healthBar){
		healthBar = _healthBar;
	}

	//Reload bar object
	public GameObject getReloadBar(){
		return reloadBar;
	}

	public void setReloadBar(GameObject _reloadBar){
		reloadBar = _reloadBar;
	}

	//Enemy Health Bar object
	public GameObject getEnemyHBar(){
		return enemyHBar;
	}

	public void setEnemyHBar(GameObject _enemyHBar){
		enemyHBar = _enemyHBar;
	}

	//Clock object
	public GameObject getPClock(){
		return playerClock;
	}

	public void setPClock(GameObject _playerClock){
		playerClock = _playerClock;
	}

	//Clock text
	public Text getClockText(){
		return clockText;
	}

	public void setClockText(Text _clockText){
		clockText = _clockText;
	}

	//Damage display object
	public GameObject getWWE(){
		return damageDisplay;
	}

	public void setWWE(GameObject _damageDisplay){
		damageDisplay = _damageDisplay;
	}

	//Bullet object (if xHair and Bullet are both dependent on it)
	public GameObject getBulletType(){
		return bulletType;
	}

	public void setBulletType(GameObject _bulletType){
		bulletType = _bulletType;
	}

	//Crosshair object
	public GameObject getXHair(){
		return xHair;
	}

	public void setXHair(GameObject _xHair){
		xHair = _xHair;
	}

	//Bullet object
	public GameObject getBullet(){
		return bullet;
	}

	public void setBullet(GameObject _bullet){
		bullet = _bullet;
	}

	#endregion

	// Initialises the active model that is specified from Photon Network with the hard-coded factors that influence player UI
	public void initPlayer(int playerToInit, string playerOrEnemy){ // 0: Split, 1: Chim, 2: Kronos, 3: Butch
		Debug.Log("init "+playerOrEnemy);
		myNameInHierachy = gameObject.name;
		allowInitUI = true;

		if (playerOrEnemy == "player") {
			Debug.Log("init "+"player");
			int playerCharacterId = playerToInit;
			if (playerCharacterId == 0) { // Split
				split.SetActive(true);
				setActiveCharacter (GameObject.Find (myNameInHierachy + "/Player1/Split").gameObject);
				getActiveCharacter ().SetActive (true);
				scriptOfActiveChar = activeCharacter.GetComponent<CharacterParameters> ();
				scriptOfActiveChar.initCharacter ("Split");
				setActiveCharId (0);
			} else if (playerCharacterId == 1) { // Chim
				chim.SetActive(true);
				setActiveCharacter (GameObject.Find (myNameInHierachy + "/Player1/Chim").gameObject);
				getActiveCharacter ().SetActive (true);
				scriptOfActiveChar = activeCharacter.GetComponent<CharacterParameters> ();
				scriptOfActiveChar.initCharacter ("Chim");
				setActiveCharId (1);
			} else if (playerCharacterId == 2) { // Kronos
				kronos.SetActive(true);
				setActiveCharacter (GameObject.Find (myNameInHierachy + "/Player1/Kronos").gameObject);
				getActiveCharacter ().SetActive (true);
				scriptOfActiveChar = activeCharacter.GetComponent<CharacterParameters> ();
				scriptOfActiveChar.initCharacter ("Kronos");
				setActiveCharId (2);
			} else if (playerCharacterId == 3) { // Butch
				butch.SetActive(true);
				setActiveCharacter (GameObject.Find (myNameInHierachy + "/Player1/Butch").gameObject);
				getActiveCharacter ().SetActive (true);
				scriptOfActiveChar = activeCharacter.GetComponent<CharacterParameters> ();
				scriptOfActiveChar.initCharacter ("Butch");
				setActiveCharId (3);
			} else {
			}
		} else if (playerOrEnemy == "enemy") {
			Debug.Log("init "+"enemy");
			int enemyCharacterId = playerToInit;
			if (enemyCharacterId == 0) { // Split
				splitEnemy.SetActive(true);
				setActiveEnemy(GameObject.Find (myNameInHierachy + "/AI/Split").gameObject);
				getActiveEnemy().SetActive(true);
				scriptOfActiveEnemy = activeEnemy.GetComponent<CharacterParameters> ();
				scriptOfActiveEnemy.initCharacter("Split");
				setEnemyId (0);
				//Debug.Log ("This should be Split's health (200) : " + scriptOfActiveEnemy.health);
			} else if (enemyCharacterId == 1) { // Chim
				chimEnemy.SetActive(true);
				setActiveEnemy(GameObject.Find (myNameInHierachy + "/AI/Chim").gameObject);
				getActiveEnemy().SetActive(true);
				scriptOfActiveEnemy = activeEnemy.GetComponent<CharacterParameters> ();
				scriptOfActiveEnemy.initCharacter("Chim");
				setEnemyId (1);
			} else if (enemyCharacterId == 2) { // Kronos
				kronosEnemy.SetActive(true);
				setActiveEnemy(GameObject.Find (myNameInHierachy + "/AI/Kronos").gameObject);
				getActiveEnemy().SetActive(true);
				scriptOfActiveEnemy = activeEnemy.GetComponent<CharacterParameters> ();
				scriptOfActiveEnemy.initCharacter("Kronos");
				setEnemyId (2);
			} else if (enemyCharacterId == 3) { // Butch
				butchEnemy.SetActive(true);
				setActiveEnemy(GameObject.Find (myNameInHierachy + "/AI/Butch").gameObject);
				getActiveEnemy().SetActive(true);
				scriptOfActiveEnemy = activeEnemy.GetComponent<CharacterParameters> ();
				scriptOfActiveEnemy.initCharacter("Butch");
				setEnemyId (3);
			} else {
			}
		} else {
			Debug.LogError ("Failed to activate character.");
			allowInitUI = false;
			return;
		}

		if (allowInitUI) {
			initPlayerUI (playerOrEnemy);
		} else {
			Debug.LogError ("UI not allowed to be updated. initPlayer called incorrectly cases above fail");
		}




		/*myNameInHierachy = gameObject.name;
		allowInitUI = true;
		switch(playerToInit) 
		{
		case 0:
			if (GameObject.Find (myNameInHierachy + "/Player1/Split") && !GameObject.Find (myNameInHierachy + "/Player1/Split").activeInHierarchy &&) {
				setActiveCharacter (GameObject.Find (myNameInHierachy + "/Player1/Split").gameObject);
				getActiveCharacter ().SetActive (true);
				scriptOfActiveChar = activeCharacter.GetComponent<CharacterParameters> ();
				scriptOfActiveChar.initCharacter ("Split");
				setActiveCharId (0);
				//Debug.Log ("This should be Split's health (200) : " + scriptOfActiveChar.health);
			} else if (GameObject.Find (myNameInHierachy + "/AI/Split") && !GameObject.Find (myNameInHierachy + "/AI/Split").activeInHierarchy) {
				setActiveEnemy(GameObject.Find (myNameInHierachy + "/AI/Split").gameObject);
				getActiveEnemy().SetActive(true);
				scriptOfActiveEnemy = activeEnemy.GetComponent<CharacterParameters> ();
				scriptOfActiveEnemy.initCharacter("Split");
				setEnemyId (0);
				//Debug.Log ("This should be Split's health (200) : " + scriptOfActiveEnemy.health);
			}
			break;
		case 1:
			if (GameObject.Find (myNameInHierachy + "/Player1/Chim") && !GameObject.Find (myNameInHierachy + "/Player1/Chim").activeInHierarchy) {
				setActiveCharacter (GameObject.Find (myNameInHierachy + "/Player1/Chim").gameObject);
				getActiveCharacter ().SetActive (true);
				scriptOfActiveChar = activeCharacter.GetComponent<CharacterParameters> ();
				scriptOfActiveChar.initCharacter ("Chim");
				setActiveCharId (1);
			} else if (GameObject.Find (myNameInHierachy + "/AI/Chim") && !GameObject.Find (myNameInHierachy + "/AI/Chim").activeInHierarchy) {
				setActiveEnemy(GameObject.Find (myNameInHierachy + "/AI/Chim").gameObject);
				getActiveEnemy().SetActive(true);
				scriptOfActiveEnemy = activeEnemy.GetComponent<CharacterParameters> ();
				scriptOfActiveEnemy.initCharacter("Chim");
				setEnemyId (1);
			}

			break;
		case 2:
			if (GameObject.Find (myNameInHierachy + "/Player1/Kronos") && !GameObject.Find (myNameInHierachy + "/Player1/Kronos").activeInHierarchy) {
				setActiveCharacter (GameObject.Find (myNameInHierachy + "/Player1/Kronos").gameObject);
				getActiveCharacter ().SetActive (true);
				scriptOfActiveChar = activeCharacter.GetComponent<CharacterParameters> ();
				scriptOfActiveChar.initCharacter ("Kronos");
				setActiveCharId (2);
			} else if (GameObject.Find (myNameInHierachy + "/AI/Kronos") && !GameObject.Find (myNameInHierachy + "/AI/Kronos").activeInHierarchy) {
				setActiveEnemy(GameObject.Find (myNameInHierachy + "/AI/Kronos").gameObject);
				getActiveEnemy().SetActive(true);
				scriptOfActiveEnemy = activeEnemy.GetComponent<CharacterParameters> ();
				scriptOfActiveEnemy.initCharacter("Kronos");
				setEnemyId (2);
			}
			break;
		case 3:
			if (GameObject.Find (myNameInHierachy + "/Player1/Butch") && !GameObject.Find (myNameInHierachy + "/Player1/Butch").activeInHierarchy) {
				setActiveCharacter (GameObject.Find (myNameInHierachy + "/Player1/Butch").gameObject);
				getActiveCharacter ().SetActive (true);
				scriptOfActiveChar = activeCharacter.GetComponent<CharacterParameters> ();
				scriptOfActiveChar.initCharacter ("Butch");
				setActiveCharId (3);
			} else if (GameObject.Find (myNameInHierachy + "/AI/Butch") && !GameObject.Find (myNameInHierachy + "/AI/Butch").activeInHierarchy) {
				setActiveEnemy(GameObject.Find (myNameInHierachy + "/AI/Butch").gameObject);
				getActiveEnemy().SetActive(true);
				scriptOfActiveEnemy = activeEnemy.GetComponent<CharacterParameters> ();
				scriptOfActiveEnemy.initCharacter("Butch");
				setEnemyId (3);
			}
			break;
		default:
				Debug.LogError ("No character called from network init set to active.");
				allowInitUI = false;
			break;
		}
		if (allowInitUI) {
			initPlayerUI (playerOrEnemy);
		} else {
			Debug.LogError ("UI not allowed to be updated. initPlayer called incorrectly cases above fail");
		}*/
	}

	public void initPlayerUI(string playerOrEnemy){
		timeLeft = 30;
		if (playerOrEnemy == "player") {
			initHealthbar ();
			// initReloadbar ();
			initReload ();
			initPClock ();
			// initJoystick();
		} else if (playerOrEnemy == "enemy") {
			initEnemyHbar ();
			//startTimer = true;
		} else {
			Debug.LogError ("Wrong string passed");
		}

		if (!disableShooting) {
			initXHair ();
			if (playerOrEnemy == "player") {
				initBullet ();
			} else if (playerOrEnemy == "enemy") {
				initEnemyBullet ();
			} else {
				Debug.LogError ("Wrong string passed");
			}
		}
	}

	public void initHealthbar(){
		//Debug.Log ("health"+scriptOfActiveChar.health.ToString());
		string searchString = myNameInHierachy+"/PlayerUI/Healthbar";
		setHealthBar (GameObject.Find (searchString).gameObject);
		//healthBar.gameObject.GetComponentInChildren<Text>().text = "Your Health: "+scriptOfActiveChar.health.ToString();
		healthBar.gameObject.GetComponentInChildren<Text>().text = "YOUR HEALTH";
	}

	public void initEnemyHbar(){
		//Debug.Log ("enemy"+scriptOfActiveChar.health.ToString());
		string searchString = myNameInHierachy+"/PlayerUI/EnemyHealthBar";
		setEnemyHBar (GameObject.Find (searchString).gameObject);
		//enemyHBar.gameObject.GetComponentInChildren<Text>().text = "Enemy Health: "+ scriptOfActiveEnemy.health.ToString();
		enemyHBar.gameObject.GetComponentInChildren<Text>().text = "ENEMY HEALTH";
	}

	public void initReloadbar(){
		//GameObject.Find (myNameInHierachy+"/PlayerUI/Reloadbar").gameObject
		//Debug.LogError ("reload");
		string searchString = myNameInHierachy+"/PlayerUI/Reloadbar";
		setReloadBar (GameObject.Find (searchString).gameObject);
		reloadBar.gameObject.GetComponentInChildren<Text>().text = "";
		//setPClock(GameObject.Find (searchString).gameObject);
		//playerClock.gameObject.GetComponentInChildren<Text> ().text = "Grace period. Get to position!";
		isReloading = false;
	}

	public void initReload() {
		isReloading = false;
		string searchString = myNameInHierachy+"/PlayerUI/ReloadText";
		GameObject.Find (searchString).gameObject.GetComponentInChildren<Text>().text = "";
	}

	public void shootReload() {
		//Debug.LogError ("Shoot Reload -Start");
		StartCoroutine (reloadWait());
		//whatever();
		//GameObject.Find (searchString).gameObject.GetComponentInChildren<Text>().text = "";
		//isReloading = false;
		//Debug.LogError (isReloading);
		//Debug.LogError ("Shoot Reload -Done");
	}

	IEnumerator reloadWait () {
		isReloading = true;
		string searchString = myNameInHierachy+"/PlayerUI/ReloadText";
		GameObject.Find (searchString).gameObject.GetComponentInChildren<Text>().text = "R";
		yield return new WaitForSecondsRealtime (2);
		GameObject.Find (searchString).gameObject.GetComponentInChildren<Text>().text = "";
		isReloading = false;
	}

	void whatever(){
		reloadWait ();
	}

	public void initPClock(){
		string searchString = myNameInHierachy+"/PlayerUI/Clock";
		setPClock(GameObject.Find (searchString).gameObject);
		setClockText(playerClock.gameObject.GetComponentInChildren<Text> ());
		changeClockText (getPClock (), matchDuration, gracePeriodDuration, true);	
	}

	public void initTimer () {
		
	}

	public void initDDisplay(){

	}

	public void initBulletType(){

	}

	public void initXHair(){
		//Debug.Log (gameObject.name + " " + player);
		string searchString = gameObject.name + "/PlayerUI/MovableXHairArea/Crosshair";
		setXHair (GameObject.Find (searchString));
		xHair.gameObject.GetComponent<RawImage> ().texture = xHairTextures [activeCharId];
		scriptOfXHair = xHair.gameObject.GetComponent<CrosshairScript> (); // May be needed for collision logic later
	}

	public void initBullet(){
		//setBullet(xHair.transform.GetChild(activeCharId).gameObject);
		//Debug.Log ("Active? " + getBullet ().activeSelf.ToString ());
		//scriptOfXHair.initMyBulletType(activeCharId);
		//scriptOfPlayerHitBox = xHair.transform.GetChild (activeCharId).gameObject.GetComponent<bulletHole> ();
	}

	public void initEnemyBullet(){
		// scriptOfAIHitbox = activeEnemy.transform.GetChild (0).GetComponentInChildren<bulletHole>();
		//Try the below if it doesn't work.
		//scriptOfAIHitbox = activeEnemy.GetComponentInChildren<bulletHole> ();
	}

	public void updatehealthbars (int playerdmg, int oppdmg) {
		//Debug.LogError ("Reduce Player health by " + damage.ToString());
		//getHealthBar().gameObject.GetComponentInChildren<Text>().text = "Your Health: "+(scriptOfActiveChar.health-damage).ToString();
		getHealthBar().gameObject.GetComponentInChildren<Text>().text = "";
		float baseHealth = (float)scriptOfActiveChar.health;
		float updatedHealth = baseHealth - (float)playerdmg;
		float factor = updatedHealth / baseHealth;
		float newWidth = 320 * factor;
		getHealthBar ().gameObject.GetComponentInChildren<RectTransform> ().sizeDelta = new Vector2 (newWidth, 40f);
		if (scriptOfActiveChar.health < playerdmg) {
			networkManager.playerLoss ();
		} else {
		}

		//Debug.LogError ("Reduce Enemy health by " + damage.ToString());
		//getEnemyHBar().gameObject.GetComponentInChildren<Text>().text = "Enemy Health: "+(scriptOfActiveEnemy.health-damage).ToString();
		getEnemyHBar().gameObject.GetComponentInChildren<Text>().text = "";
		baseHealth = (float) scriptOfActiveEnemy.health;
		updatedHealth = baseHealth - (float) oppdmg + 10;
		factor = updatedHealth / baseHealth;
		newWidth = 320 * factor;
		getEnemyHBar().gameObject.GetComponentInChildren<RectTransform>().sizeDelta = new Vector2 (newWidth,40f);
		if (scriptOfActiveEnemy.health < oppdmg) {
			// Network end game
			networkManager.playerWon ();
		} else {
		}


	}

	public void reducePlayerHealth(int damage){
		//Debug.LogError ("Reduce Player health by " + damage.ToString());
		//getHealthBar().gameObject.GetComponentInChildren<Text>().text = "Your Health: "+(scriptOfActiveChar.health-damage).ToString();
		getHealthBar().gameObject.GetComponentInChildren<Text>().text = "";
		float baseHealth = (float)scriptOfActiveChar.health;
		float updatedHealth = baseHealth - (float)damage;
		float factor = updatedHealth / baseHealth;
		float newWidth = 320 * factor;
		getHealthBar ().gameObject.GetComponentInChildren<RectTransform> ().sizeDelta = new Vector2 (newWidth, 40f);
		if (scriptOfActiveChar.health < damage) {
			networkManager.playerLoss ();
		} else {
		}
	}

	public void reduceEnemyHealth(int damage){
		//Debug.LogError ("Reduce Enemy health by " + damage.ToString());
		//getEnemyHBar().gameObject.GetComponentInChildren<Text>().text = "Enemy Health: "+(scriptOfActiveEnemy.health-damage).ToString();
		getEnemyHBar().gameObject.GetComponentInChildren<Text>().text = "";
		float baseHealth = (float) scriptOfActiveEnemy.health;
		float updatedHealth = baseHealth - (float) damage;
		float factor = updatedHealth / baseHealth;
		//Debug.Log ("factor"+factor.ToString());
		float newWidth = 320 * factor;
		getEnemyHBar().gameObject.GetComponentInChildren<RectTransform>().sizeDelta = new Vector2 (newWidth,40f);
		if (scriptOfActiveEnemy.health < damage) {
			// Network end game
			networkManager.playerWon ();
		} else {
		}
	}

	public void updateReload(){

	}

	public void changeClockText(GameObject _clock, float _timeRemaining, float delayTime, bool isGrace){
		if (isGrace) {
			getClockText().text = "Get to position!";
		} else {
			string minutes = Mathf.Floor (_timeRemaining / 60).ToString ("00");
			string seconds = Mathf.Floor(_timeRemaining % 60).ToString("00");

			getClockText ().text = minutes + ":" + seconds;

		}
	}
		
	private void endMatch(){
		//End game logic here. Has to communicate back to network (win streak)
		gameOver = true;
		Debug.Log("Match Ended");
		//Game over UI?
		networkManager.playerLoss();
	}

	#region Debug Log strings
	//Debug.Log ("This is the searchString:" + searchString);
	#endregion

}
