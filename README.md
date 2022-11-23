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
			- ✔ tutorial- push rocks into holes
			- push rocks into symmetric pattern as inaccessible part
			- push rocks into 4 corners of diamond island
			- sokobans
			- push rock across 1 tile corner from boat
			- talk to rock NPC next to hole multiple times
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
			- frog- explains talking/shops
			- ✔ snake- explains monetary increments
				- has one of each coin above him
			- ✔ turtle- sells hat
				- I found this cool hat on the beach.
				- I don't need it, because my whole body is a hat.
				- I'll sell it to you for 100 gold. >Buy/Don't
				- [decline] That's alright. When you're as old as I am, rejection doesn't bother you anymore.
				- [not enough] I may be old, but I can see that you don't have enough gold.
				- [buy] A kitten in a hat... this'll be one to write home about.
				- [bought] To be honest, I don't know whether I'm a turtle or a tortoise.
			- ✔ flamingo- sells boots
				- Flamingos are natural experts on footwear.
				- That's why we stand on one leg all the time. So we don't wear out our shoes.
				- You can buy these stylish boots, for only 75 gold. >Buy/Don't
				- [decline] That's okay. They're too hip for you anyway.
				- [not enough] You don't have enough gold, and I don't do discounts.
				- [buy] Wow. You're serving already.
				- [bought] My leg is getting tired...
			- ✔ parrot- sells eyepatch
				- Caw! The most notorious pirates all wear eye patches!
				- Because if they can plunder without depth perception, they must be a fearsome foe indeed!
				- You can buy this eyepatch for a mere 25 gold! >Buy/Don't
				- [decline] Suit yourself! Caw!
				- [not enough] You tryin' to scam me? Caw! You need more gold!
				- [buy] You'll be the terror of the high seas!
				- [bought] I once served under Captain Blackbeard himself!
				-	...but only after the name entered public domain.
			- ✔ octopus- sells hook
			- ✔ sea serpent- sells reinforced hull
				- custom shopkeeper script- no crab spawn (he is on the shore)
				- [not all 4 pirate items] I only speak to real pirates. You look like a landlubber.
				- Wow, you look like a real pirate. Be warned, choppy seas out there...
				- I can reinforce your ship's hull to navigate the deeper waters.
				- But it'll cost you 250 gold. >Upgrade/Don't
				- [decline] Not everyone is cut out for adventure.
				- [not enough] Who are you trying to fool? Come back when you have 250 gold.
				- [buy] Good luck out there, and watch out for sharks...
				- [bought] I'm hungry... I wonder if Panera Bread takes pirate gold.
			- otter- sends letter to penguin
			- penguin- receives letter
			- *mermaid?*- tells you about hidden treasure
			- *?*- sells map?
			- *?*- mad scientist operating jelly zapper (farm piranhas for gold)
			- *monkey*- dances directions of gems
			- ✔ starfish cultist (in deepwater)- sells jellyfish artifact
				- His coming marks the end of days.
				- If you wish to see him, you'll have to pay.
				- [Buy the Jellyfish Statue for 500 gold?] Yes/No
				- [decline] (nothing)
				- [not enough] [Not enough gold.]
				- [buy] (nothing)
				- [bought] Across the oceans, he shall swim.
				- 	With this chant, I honor him.
				- [killed jellyfish] You did WHAT?!?
			- ✔ manatee- sells Winner's Trophy for 9999 coins
				- I won this trophy at a foosball tournament! It says "You're a True Winner!"
				- You can buy it... for only 9999 gold! Hah! As if you'll ever get that much!
				- [Buy the Winner's Trophy?] Buy/Don't
				- [decline] I knew it! No one has that much gold!
				- [not enough] I said 9999 gold, pal! No discounts!
				- [buy] What... you're serious? I was just kidding, but all right... here you go!
				- (I'm rich!!!)
				- [bought] (I'm rich!!!)
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
	- write & test otter/penguin dialog
	- puzzles
		- condition- all hidden spots filled
			- test
		- sokobans
	- allow push boulder into cave
		- move npcs onto their own layer
		- make crab, pushable check for that layer instead of default
	- world
		- lay out islands
			- hidden spot puzzles
		- make sure crab works for each shopkeeper
		- place hub music & deepwater music zones
		- distribute gold (500 each in shallow & deep)
		- deepwater barriers
			- reinforced hull destroy deepwater barriers on create
		- clean up island edges
		- fill with water
		- surround with rocks
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
