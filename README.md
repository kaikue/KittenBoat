# Kitten Boat

For Sylvie's Jam #1 https://itch.io/jam/sylvies-jam-1

A game that you hate but it has a cute kitten in it, so it's impossible to insult the kitten. The kitten can ride a boat sometimes. When you try to buy from the shop there's a random chance something bad happens. The boat is hard to control precisely. When you die, you have to rebuild the boat by playing a puzzle game style sequence. There is an enemy (jellyfish) who appears when the timer runs out and tries to get you. There must be island gameplay and boat gameplay. The goal is only to get money and buy expensive items.

## TODO

- [x] A game that you hate but it has a cute kitten in it, so it's impossible to insult the kitten.
- [x] The kitten can ride a boat sometimes.
- [x] When you try to buy from the shop there's a random chance something bad happens.
- [x] The boat is hard to control precisely.
- [ ] There must be island gameplay and boat gameplay.
	- island gameplay
		- puzzles:
			- ✔ tutorial- push rock into hole
			- ✔ push rocks into 4 corner pits of diamond island
			- ✔ push rocks into matching pattern as inaccessible part
			- sokobans
				- fill in middle hole first to make path to right one
			- push rock across 1 tile corner from boat
			- ✔ talk to rock NPC next to hole multiple times
			- facilitate npc romance
			- secondary cave inside cave- solve puzzle, reset, push rock into cave
			- buried treasure- dig at X
			- ~~drop rocks into water to form path?~~
		- deepwater islands (need upgrade): colored gems
			- push gems into 4 corners of diamond island corresponding to boat directions
			- push gems into order according to npc dance
			- dig for buried gem from hint
				- npc on one island tells you about buried treasure on another
			- secret path in rock spikes
			- lure enemy, break boat between islands, push rock across, repair boat
				- easier version: smash against fixed hazard
			- hit reset button partway through (cube respawn)
				- separate reset buttons per color gem
			- sokobans
				- gems can fill wrong color holes- push red over green in red hole to accomplish something
			- push gem onto reset button
			- jelly zapper machine- farm piranhas
			- ~~smuggle gem on boat?~~
			- ~~shopkeeper that only takes gems?~~
	- boat gameplay
		- boat breaks into tiles, need to repair
		- dodge rocks
		- dodge shark patrols
		- run from chasing piranhas (despawn when too far away)
		- ~~wind?~~
		- jellyfish
- [ ] The goal is only to get money and buy expensive items.
	- ~~maybe a shopkeeper sells a second item or upgrade afterwards~~
	- ~~use gems directly? smuggling?~~
	- shop
		- shopkeepers
			- ✔ frog
			- ✔ snake- has one of each coin above him
			- ✔ turtle
			- ✔ flamingo
			- ✔ parrot
			- ✔ octopus
			- ✔ manatee
			- ✔ sea serpent
			- ✔ otter
			- ✔ penguin
			- ✔ starfish cultist
			- *mermaid?*- tells you about hidden treasure
			- *?*- sells map?
			- chameleon- mad scientist operating jelly zapper (farm piranhas for gold)
				- I hate piranhas so freakin much!!!
				- That's why I built this giant laser! To blow them all up!
				- If you help blow up 5 piranhas I'll give you a reward!
				- [retalk] Remember, 5 piranhas! Zap em with the laser!!!
				- [complete] Ahh, the beautiful smell of fried piranha!
				- Thanks pal! Help yourself to this treasure!
				- [complete retalk] Feel free to keep killing piranhas if you want!
				- [killed jellyfish] Uhh... what just happened?
			- *monkey*- dances directions of gems
			- ~~*wizard?*- I am the wizard! Don't touch my Orb!~~
	- progression
		- all 4 pirate clothes (25+50+75+100=250)
		- boat upgrade (250)
		- jellyfish statue (500)
		- winner's trophy (9999)
- [ ] When you die, you have to rebuild the boat by playing a puzzle game style sequence.
	- interact prompt
	- boat trivia?
	- turn blocks puzzle?
- [ ] There is an enemy (jellyfish) who appears when the timer runs out and tries to get you.
	- place jellyfish trophy on island at bottom left corner (pedestal npc)
	- get back on board and timer starts
	- race away from it all the way across the map to top right corner where you can kill it (no dismounting boat)
		- if it gets you you have to rebuild (puzzle) & get a little headstart
	- when killed drops 9999 gold & platinum & gems
		- set killedJellyfish = true

- Misc
	- more decoration tiles
	- laser puzzle
		- test chameleon dialog
		- piranhas
			- spawn
			- animated
			- chase you & point towards you
			- despawn if too far away
			- sounds?
	- puzzles
		- sokobans
		- dig for treasure- invisible npc
	- world
		- lay out islands
		- decorate with rocks
		- make sure crab works for each shopkeeper
		- place hub music & deepwater music zones
		- distribute gold (500 each in shallow & deep)
		- deepwater barriers
			- reinforced hull destroy deepwater barriers on create
		- clean up island edges
		- fill with water
		- surround with rocks
		- kitten at 0,0
	- boat stuff
		- patrolling sharks
			- open & close mouths
			- move between points (square or back/forth)
				- angle/flip to face next point
		- chasing piranhas
		- puzzle game style sequence to rebuild? (shop music)
		- floating treasure piles?
	- winner's trophy- on obtain, go to end screen?
	- button tutorials
		- movement
		- interact (first NPC)
	- start/end menus
		- menu music- Pirate1 Theme1
		- ending music- Treasure Hunter
	- fix button prompt starting as gamepad? (global check)
	- animate water tiles?
- Sounds
	- button press/unpress
	- cat meows?

## Credits

Font: Softsquare Mono by Chevy Ray https://chevyray.itch.io/pixel-fonts

###Sounds

- Ambience, Seaside Waves, Close, A by InspectorJ (www.jshaw.co.uk) of Freesound.org: https://freesound.org/people/InspectorJ/sounds/400632/
- https://opengameart.org/content/level-up-power-up-coin-get-13-sounds
- https://freesound.org/people/j1987/sounds/95001/
- https://freesound.org/people/Reitanna/sounds/332668/
- https://freesound.org/people/D4XX/sounds/567269/
- Some sound effects generated with SFXR: https://www.drpetter.se/project_sfxr.html

### Music

- Pirate Theme by loliver1814: https://opengameart.org/content/pirate-theme
- Of Puzzles and Treasure by Eric Matyas www.soundimage.org: https://opengameart.org/content/of-puzzles-and-treasure
- A sailor's chant by Thimras: https://opengameart.org/content/a-sailors-chant
- White Sands Day Night by MegaJackie: https://opengameart.org/content/white-sands-day-night
- Pirate Game Tune by Tozan: https://opengameart.org/content/pirate-game-tune
- Countdown to Myocardial Infarction by Peter Gresser: https://opengameart.org/content/countdown-to-myocardial-infarction
- RPG_Village_1 by Hitctrl: https://opengameart.org/content/rpgvillage1
- Blackmoor Tides (Epic Pirate Battle Theme) by Matthew Pablo: https://opengameart.org/content/blackmoor-tides-epic-pirate-battle-theme
- Treasure Hunter by TAD: https://opengameart.org/content/treasure-hunter


bonus links:
https://opengameart.org/content/summer-resort-theme
https://opengameart.org/users/thimras
