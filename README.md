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
		- talkable npcs
			- choices that can trigger different actions
		- puzzles:
			- push rocks into holes
			- push rocks into symmetric pattern as inaccessible part
			- push rocks into 4 corners of diamond island
			- drop rocks into water to form path
			- talk to rock NPC next to hole multiple times ("Oh, I'm just a rock..." "You want me to move? No, I'm comfortable here...")
			- park boat between islands, push rock across
			- secondary cave inside cave- solve puzzle, reset, push rock into cave
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
		- dodge rocks
		- dodge shark patrols
		- wind?
		- jellyfish
[ ] The goal is only to get money and buy expensive items.
	- each shopkeeper sells item(s) through dialog- choose item or cancel
		- maybe a shopkeeper sells a second item or upgrade afterwards
	- when you buy an item there's a 30% chance a crab will steal it and you have to chase down the crab
		- the crab chooses a few random grid paths, turning randomly a few times until it hits the wall, and whichever one has the furthest average distance from the player it follows. It repeats once it hits the wall. You get the item when you catch it
	- the good items look like youll have to grind a lot but theres an island at the end with a lot of platinum?
	- use gems directly? smuggling?
	- shop
		- shopkeeper(s?)- octopus? frog? turtle? flamingo?
		- items
			- Pirate Hat
			- Eye Patch
			- Tail Hook
			- Boots
				- animate with walk
			- Strengthened Hull (deepwater upgrade)
			- Winner's Trophy
[ ] When you die, you have to rebuild the boat by playing a puzzle game style sequence.
	- boat trivia?
[ ] When you try to buy from the shop there's a random chance something bad happens.
[ ] There is an enemy (jellyfish) who appears when the timer runs out and tries to get you.
	- the jellyfish has a funny evil poem he says to you?
	- epic jellyfish boss battle at the end?? buy something that starts the timer
		- shoot cannons by stepping on buttons

- Misc
	- dialog
		- background
		- choices
		- fix button prompt starting as gamepad (global check)
		- make text appear instantly
	- unmute music
	- set island music based on area
	- start/end menus
		- menu music- Pirate1 Theme1
		- ending music- Treasure Hunter
	- animate water tiles?
- Sounds
	- waves!
	- coin collect
	- button press/unpress
	- rock push
	- fill pit
	- reset button activate
	- cat meows?

## Credits

Font: Softsquare Mono by Chevy Ray https://chevyray.itch.io/pixel-fonts

### Music

- Pirate Theme by loliver1814: https://opengameart.org/content/pirate-theme
- Of Puzzles and Treasure by Eric Matyas www.soundimage.org: https://opengameart.org/content/of-puzzles-and-treasure
- A sailor's chant by Thimras: https://opengameart.org/content/a-sailors-chant
- White Sands Day Night by MegaJackie: https://opengameart.org/content/white-sands-day-night
- Pirate Game Tune by Tozan: https://opengameart.org/content/pirate-game-tune
- RPG_Village_1 by Hitctrl: https://opengameart.org/content/rpgvillage1
- Blackmoor Tides (Epic Pirate Battle Theme) by Matthew Pablo: https://opengameart.org/content/blackmoor-tides-epic-pirate-battle-theme
- Treasure Hunter by TAD: https://opengameart.org/content/treasure-hunter