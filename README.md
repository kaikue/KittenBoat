# Kitten Boat

For Sylvie's Jam #1 https://itch.io/jam/sylvies-jam-1

A game that you hate but it has a cute kitten in it, so it's impossible to insult the kitten. The kitten can ride a boat sometimes. When you try to buy from the shop there's a random chance something bad happens. The boat is hard to control precisely. When you die, you have to rebuild the boat by playing a puzzle game style sequence. There is an enemy (jellyfish) who appears when the timer runs out and tries to get you. There must be island gameplay and boat gameplay. The goal is only to get money and buy expensive items.

## TODO

[x] A game that you hate but it has a cute kitten in it, so it's impossible to insult the kitten.
[x] The kitten can ride a boat sometimes.
[x] The boat is hard to control precisely.
[ ] There must be island gameplay and boat gameplay.
	- island gameplay
		- open up cave doors
			- each puzzle has its own condition (script derived from parent puzzle class)
			- caves contain treasure
		- puzzles:
			- push rocks into holes
			- push rocks into symmetric pattern as inaccessible part
			- push rocks into 4 corners of diamond island
			- drop rocks into water to form path
			- push rock across 1 tile corner from boat
			- talk to rock NPC next to hole multiple times ("Oh, I'm just a rock..." "You want me to move? No, I'm comfortable here...")
			- secondary cave inside cave- solve puzzle, reset, push rock into cave
			- lure enemy, break boat between islands, push rock across, repair boat
				- easier version: smash against fixed hazard
			- push rock onto reset button?
			- hit reset button partway through (cube respawn?)
			- drop rock onto npc in water?
			- dig for buried treasure??
			- secret path in rock spikes
			- npc on one island tells you about solution/buried treasure on another
			- facilitate npc romance
		- deepwater islands (need upgrade): colored gems
			- push gems into 4 corners of diamond island corresponding to boat directions
			- push gems into order according to npc dance
			- smuggle gem on boat
			- shopkeeper that only takes gems?
	- boat gameplay
		- boat breaks into tiles, need to repair
		- dodge rocks
		- dodge shark patrols
		- wind?
		- jellyfish
[ ] The goal is only to get money and buy expensive items.
	- each shopkeeper sells item(s) through dialog- choose item or cancel
		- maybe a shopkeeper sells a second item or upgrade afterwards
	- the good items look like youll have to grind a lot but theres an island at the end with a lot of platinum?
	- use gems directly? smuggling?
	- shop
		- shopkeepers
			- frog
			- turtle- sells hat
			- flamingo- sells boots
			- octopus- sells hook
			- *parrot*- sells eyepatch
			- *snake*
			- *monkey*
			- *?*- sends letter to penguin
			- penguin- receives letter
		- items
			- Pirate Hat
			- Eye Patch
			- Tail Hook
			- Boots
				- animate with walk
			- Strengthened Hull (deepwater upgrade)
			- Winner's Trophy
[ ] When you die, you have to rebuild the boat by playing a puzzle game style sequence.
	- interact prompt
	- boat trivia?
[ ] When you try to buy from the shop there's a random chance something bad happens.
	- when you buy an item there's a 30% chance a crab will steal it and you have to chase down the crab (give him a little head start)
[ ] There is an enemy (jellyfish) who appears when the timer runs out and tries to get you.
	- the jellyfish has a funny evil poem he says to you?
	- epic jellyfish boss battle at the end?? buy something that starts the timer
		- shoot cannons by stepping on buttons

- Misc
	- puzzles
	- unmute music
	- button tutorials
		- movement
		- interact (first NPC)
	- set island music based on area
	- start/end menus
		- menu music- Pirate1 Theme1
		- ending music- Treasure Hunter
	- fix button prompt starting as gamepad? (global check)
	- animate water tiles?
- Sounds
	- coin collect
	- button press/unpress
	- rock push
	- fill pit
	- reset button activate
	- cat meows?

## Credits

Font: Softsquare Mono by Chevy Ray https://chevyray.itch.io/pixel-fonts

Wave sound: Ambience, Seaside Waves, Close, A by InspectorJ (www.jshaw.co.uk) of Freesound.org: https://freesound.org/people/InspectorJ/sounds/400632/

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
