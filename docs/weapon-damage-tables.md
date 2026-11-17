# X and Zero weapon damage tables

These tables show effective HP damage from X and Zero attacks against non-boss, weapon-targetable objects in `src/main/mains`. Damageable hazards and destructible stage objects are included; bosses and boss parts are excluded. Target rows use the ordinary damageable table, not temporary state-specific immunity tables. For example, Spike Marl uses its uncurled table; while curled it is immune to these attacks.

All attack IDs and damage values are decimal. Each attack column is labeled with its ID. A value of `0` means no HP is removed; reaction kinds `-1` and `2` also do not apply the damage field. `main_34_highway_trooper` and `main_63_hatch_turret` have `collision_data == NULL` and therefore no table for this damage path.

`Charged Soul Body slot` (X ID 12) and `Auxiliary Lemon-type projectile` (X ID 23) are retained as ID labels; their exact in-game attack behavior is not fully confirmed.

## X

| Target (Id) | 0 X-Buster (normal) | 1 Lightning Web | 2 Frost Tower | 3 Soul Body | 4 Rising Fire | 5 Ground Hunter | 6 Aiming Laser | 7 Double Cyclone | 8 Twin Slasher | 9 X-Buster (partial charge) | 10 Charged Lightning Web | 11 Charged Frost Tower | 12 Charged Soul Body slot* | 13 Charged Rising Fire | 14 Charged Ground Hunter | 15 Charged Aiming Laser | 16 Charged Double Cyclone | 17 Charged Twin Slasher | 18 X-Buster (full charge) | 19 Stock Shot | 20 Plasma Shot (main) | 21 Plasma Shot (lingering) | 22 Nova Strike | 23 Auxiliary Lemon-type projectile* |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Armored Walker (01) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 2 | 8 | 4 | 4 | 4 | 4 | 15 | 8 | 16 | 2 | 32 | 0 |
| Item Carrier (02) | 3 | 1 | 4 | 2 | 4 | 4 | 0 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 0 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Spike Marl (03) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Drill Copter (04) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Robot Bee (05) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Bulldozer (06) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Ambush Gunner (07) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 1 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Dragonfly (10) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Wall Crawler (11) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Hover Sentry (12) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Heavy Mech (13) | 3 | 0 | 0 | 2 | 16 | 4 | 1 | 0 | 1 | 5 | 0 | 0 | 3 | 32 | 6 | 8 | 0 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Ice Bird (14) | 3 | 4 | 0 | 2 | 16 | 4 | 1 | 0 | 1 | 5 | 8 | 0 | 3 | 32 | 6 | 8 | 0 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Shell Crawler (15) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Trident Mech (16) | 3 | 16 | 4 | 2 | 4 | 0 | 1 | 0 | 0 | 5 | 16 | 8 | 3 | 8 | 0 | 8 | 0 | 0 | 15 | 8 | 16 | 2 | 32 | 0 |
| Snowman Bomb (17) | 3 | 1 | 0 | 2 | 16 | 4 | 1 | 0 | 1 | 5 | 4 | 0 | 3 | 32 | 6 | 8 | 0 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Surface Hopper (19) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Ice Block (20) | 3 | 1 | 4 | 2 | 16 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 16 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Falling Icicle (21) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Bee Hive (22) | 3 | 0 | 4 | 2 | 16 | 0 | 1 | 0 | 1 | 5 | 0 | 8 | 3 | 32 | 0 | 8 | 0 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Slope Skier (23) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Jet Drone (24) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Caterkiller (25) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Ice Wall (26) | 3 | 1 | 0 | 2 | 16 | 4 | 1 | 2 | 1 | 5 | 4 | 16 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Dash Gunner (27) | 3 | 0 | 16 | 0 | 4 | 16 | 1 | 0 | 1 | 5 | 0 | 8 | 3 | 8 | 6 | 8 | 0 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Spawner Pod (28) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Pod Spawner (29) | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 |
| Regen Turret (30) | 3 | 1 | 0 | 2 | 16 | 4 | 0 | 2 | 1 | 5 | 4 | 0 | 3 | 16 | 6 | 0 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Thorn Trap (31) | 3 | 1 | 0 | 2 | 16 | 4 | 0 | 2 | 1 | 5 | 4 | 0 | 3 | 16 | 6 | 0 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Bomb Bat (32) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Spike Sled (33) | 1 | 1 | 2 | 0 | 1 | 3 | 1 | 0 | 0 | 2 | 2 | 3 | 3 | 3 | 2 | 2 | 0 | 0 | 8 | 3 | 6 | 1 | 8 | 0 |
| Wheel Charger (35) | 3 | 1 | 4 | 2 | 4 | 4 | 0 | 2 | 6 | 5 | 4 | 8 | 3 | 8 | 8 | 0 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Latcher (36) | 0 | 0 | 0 | 32 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 32 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 |
| Rocket Spiker (37) | 3 | 16 | 4 | 0 | 16 | 4 | 1 | 0 | 0 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 0 | 0 | 15 | 8 | 16 | 2 | 32 | 0 |
| Train Cannon (38) | 3 | 6 | 1 | 0 | 1 | 4 | 1 | 0 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 0 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Falling Ceiling (39) | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 |
| Breakable Terrain (40) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Data Hopper (41) | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 |
| Homing Orb (42) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Anchored Mine (44) | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 |
| Sentry Drone (48) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Train Soldier (49) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Train Crate (50) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Fortress Cannon (51) | 3 | 8 | 0 | 0 | 0 | 4 | 1 | 0 | 1 | 5 | 8 | 0 | 3 | 0 | 6 | 8 | 0 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Jump Shooter (52) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Wave Rider (53) | 3 | 1 | 4 | 2 | 5 | 4 | 1 | 2 | 1 | 5 | 4 | 16 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 9 | 18 | 0 | 0 | 0 |
| Beam Drone (58) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Flame Jet (62) | 0 | 0 | 1 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 |
| Ride Armor Pilot (71) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Unused Ride Armor (72) | 3 | 1 | 4 | 2 | 4 | 4 | 1 | 2 | 1 | 5 | 4 | 8 | 3 | 8 | 6 | 8 | 4 | 6 | 15 | 8 | 16 | 2 | 32 | 0 |
| Spike Crawler (76) | 0 | 0 | 0 | 0 | 0 | 16 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 16 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 |

## Zero

| Target (Id) | 24 Ground saber slash | 25 Saber combo hit 2 | 26 Saber combo hit 3 | 27 Jump/fall slash | 28 Landing follow-up | 29 Shippuuga | 30 Ladder slash | 31 Wall slash | 32 Raijingeki | 33 Hyouretsuzan | 34 Spin slash | 35 Ryuenjin |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Armored Walker (01) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Item Carrier (02) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Spike Marl (03) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Drill Copter (04) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Robot Bee (05) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Bulldozer (06) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Ambush Gunner (07) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Dragonfly (10) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Wall Crawler (11) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Hover Sentry (12) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Heavy Mech (13) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 0 | 6 | 8 |
| Ice Bird (14) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Shell Crawler (15) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Trident Mech (16) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Snowman Bomb (17) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 1 | 6 | 8 |
| Surface Hopper (19) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Ice Block (20) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Falling Icicle (21) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Bee Hive (22) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 6 | 2 | 6 | 8 |
| Slope Skier (23) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Jet Drone (24) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Caterkiller (25) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Ice Wall (26) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 0 | 6 | 8 |
| Dash Gunner (27) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Spawner Pod (28) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Pod Spawner (29) | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 |
| Regen Turret (30) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 0 | 6 | 16 |
| Thorn Trap (31) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 0 | 6 | 16 |
| Bomb Bat (32) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Spike Sled (33) | 1 | 1 | 1 | 1 | 1 | 2 | 4 | 2 | 1 | 3 | 1 | 2 |
| Wheel Charger (35) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Latcher (36) | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 32 | 0 |
| Rocket Spiker (37) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Train Cannon (38) | 2 | 2 | 4 | 2 | 2 | 2 | 4 | 4 | 3 | 2 | 2 | 2 |
| Falling Ceiling (39) | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 |
| Breakable Terrain (40) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Data Hopper (41) | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 |
| Homing Orb (42) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Anchored Mine (44) | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 |
| Sentry Drone (48) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Train Soldier (49) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Train Crate (50) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Fortress Cannon (51) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 6 | 8 | 6 | 8 |
| Jump Shooter (52) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Wave Rider (53) | 3 | 3 | 2 | 2 | 2 | 2 | 2 | 2 | 1 | 0 | 0 | 0 |
| Beam Drone (58) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Flame Jet (62) | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 |
| Ride Armor Pilot (71) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Unused Ride Armor (72) | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Spike Crawler (76) | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 16 | 0 | 0 | 0 |

## Bosses and mid-bosses

These tables cover the main damage targets for boss encounters and mid-boss encounters. Boss components, adds, and flyby objects are not listed as separate targets. IDs in parentheses are the main-object IDs; attack IDs and damage values are decimal.

Each row names the `collision_data` profile used by that target. Separate rows are shown where a boss has distinct damage profiles; profile labels identify the source table without guessing its gameplay meaning. Sigma Final starts with `D_801062F0` during setup; its active targets use the two listed profiles. A damage value of `0` means no HP is removed. During temporary invulnerability, many bosses switch to `D_801060F0`, which yields zero damage for all listed attacks; those common invulnerability intervals are not repeated as rows. Storm Owl also has a special `D_80106070` collision gate. Check the boss source for exact state timing.

### X

| Target (Id / profile) | 0 X-Buster (normal) | 1 Lightning Web | 2 Frost Tower | 3 Soul Body | 4 Rising Fire | 5 Ground Hunter | 6 Aiming Laser | 7 Double Cyclone | 8 Twin Slasher | 9 X-Buster (partial charge) | 10 Charged Lightning Web | 11 Charged Frost Tower | 12 Charged Soul Body slot* | 13 Charged Rising Fire | 14 Charged Ground Hunter | 15 Charged Aiming Laser | 16 Charged Double Cyclone | 17 Charged Twin Slasher | 18 X-Buster (full charge) | 19 Stock Shot | 20 Plasma Shot (main) | 21 Plasma Shot (lingering) | 22 Nova Strike | 23 Auxiliary Lemon-type projectile* |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Background Dragon (00) - D_801062F0 | 3 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 5 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 15 | 9 | 18 | 0 | 0 | 0 |
| Eregion (08) - D_801066F0 | 1 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 2 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 5 | 9 | 18 | 0 | 0 | 0 |
| Ice Core (18) - D_80106B74 | 1 | 0 | 0 | 0 | 4 | 1 | 0 | 1 | 1 | 2 | 0 | 0 | 0 | 6 | 2 | 0 | 0 | 2 | 2 | 3 | 3 | 1 | 6 | 0 |
| Spike Sled (33) - D_801072F4 | 1 | 1 | 2 | 0 | 1 | 3 | 1 | 0 | 0 | 2 | 2 | 3 | 3 | 3 | 2 | 2 | 0 | 0 | 8 | 3 | 6 | 1 | 8 | 0 |
| Web Spider (43) - D_801075F4 | 1 | 0 | 1 | 1 | 1 | 1 | 0 | 1 | 2 | 2 | 0 | 2 | 2 | 2 | 2 | 0 | 2 | 4 | 3 | 3 | 3 | 1 | 6 | 0 |
| Train Boss (45) - D_80107678 | 2 | 6 | 1 | 0 | 1 | 4 | 0 | 0 | 1 | 3 | 4 | 4 | 3 | 4 | 6 | 4 | 0 | 6 | 6 | 6 | 6 | 1 | 6 | 0 |
| Slash Beast (54) - D_801078F8 | 1 | 1 | 1 | 0 | 1 | 4 | 0 | 0 | 0 | 2 | 1 | 0 | 0 | 0 | 5 | 0 | 0 | 0 | 3 | 3 | 3 | 1 | 6 | 0 |
| Jet Stingray (56) - D_801079F8 | 1 | 0 | 4 | 2 | 0 | 0 | 0 | 1 | 1 | 2 | 0 | 5 | 0 | 0 | 0 | 0 | 2 | 2 | 3 | 3 | 3 | 1 | 6 | 0 |
| Frost Walrus (57) - D_80107A78 | 1 | 1 | 0 | 0 | 4 | 1 | 0 | 0 | 1 | 2 | 1 | 0 | 0 | 5 | 1 | 0 | 0 | 0 | 3 | 3 | 3 | 1 | 6 | 0 |
| Storm Owl (60) - D_80107B78 | 1 | 2 | 2 | 1 | 1 | 0 | 1 | 0 | 1 | 2 | 2 | 2 | 2 | 2 | 0 | 1 | 0 | 2 | 3 | 3 | 3 | 1 | 6 | 0 |
| Split Mushroom (61) - D_80107BF8 | 1 | 4 | 1 | 0 | 1 | 1 | 0 | 1 | 1 | 2 | 2 | 1 | 0 | 1 | 1 | 0 | 1 | 1 | 3 | 3 | 3 | 1 | 6 | 0 |
| Cyber Peacock (64) - D_80107CFC | 1 | 0 | 0 | 4 | 1 | 0 | 0 | 2 | 0 | 2 | 0 | 0 | 0 | 2 | 0 | 0 | 2 | 0 | 3 | 3 | 3 | 1 | 6 | 0 |
| Magma Dragoon (65) - D_80107D7C | 1 | 2 | 0 | 0 | 0 | 0 | 0 | 4 | 0 | 2 | 2 | 0 | 0 | 0 | 0 | 0 | 5 | 0 | 3 | 3 | 3 | 1 | 6 | 0 |
| Iris (66) - profile A - D_80107DFC | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 |
| Iris (66) - profile B - D_80107E84 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 |
| Sigma (68) - profile A - D_80107F84 | 0 | 0 | 0 | 0 | 6 | 0 | 0 | 0 | 0 | 2 | 0 | 0 | 0 | 4 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 |
| Sigma (68) - profile B - D_80108004 | 1 | 4 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 2 | 4 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 3 | 3 | 3 | 1 | 6 | 0 |
| Colonel (69) - D_80108084 | 1 | 0 | 3 | 0 | 0 | 0 | 0 | 0 | 0 | 1 | 0 | 4 | 0 | 0 | 0 | 0 | 0 | 0 | 2 | 2 | 2 | 0 | 6 | 0 |
| Drone Pod (70) - D_80108104 | 1 | 8 | 2 | 2 | 4 | 4 | 1 | 2 | 1 | 2 | 4 | 8 | 3 | 8 | 6 | 8 | 4 | 6 | 4 | 4 | 4 | 1 | 6 | 0 |
| Double (73) - D_80108204 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 5 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 5 | 0 | 2 | 2 | 2 | 0 | 6 | 0 |
| Sigma Final (74) - target 1 - D_80108284 | 0 | 0 | 0 | 3 | 0 | 0 | 0 | 0 | 0 | 1 | 0 | 0 | 3 | 0 | 0 | 0 | 0 | 0 | 2 | 2 | 2 | 0 | 4 | 0 |
| Sigma Final (74) - target 2 - D_80108304 | 0 | 0 | 0 | 0 | 0 | 2 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 3 | 0 | 0 | 0 | 2 | 2 | 2 | 0 | 4 | 0 |
| General (75) - profile A - D_80108384 | 1 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 3 | 1 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 5 | 2 | 2 | 2 | 1 | 6 | 0 |
| General (75) - profile B - D_80108404 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 |

### Zero

| Target (Id / profile) | 24 Ground saber slash | 25 Saber combo hit 2 | 26 Saber combo hit 3 | 27 Jump/fall slash | 28 Landing follow-up | 29 Shippuuga | 30 Ladder slash | 31 Wall slash | 32 Raijingeki | 33 Hyouretsuzan | 34 Spin slash | 35 Ryuenjin |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Background Dragon (00) - D_801062F0 | 4 | 6 | 4 | 4 | 4 | 6 | 4 | 4 | 3 | 8 | 6 | 8 |
| Eregion (08) - D_801066F0 | 3 | 3 | 2 | 5 | 5 | 2 | 2 | 2 | 2 | 0 | 0 | 0 |
| Ice Core (18) - D_80106B74 | 2 | 2 | 1 | 1 | 1 | 2 | 2 | 2 | 0 | 0 | 2 | 3 |
| Spike Sled (33) - D_801072F4 | 1 | 1 | 1 | 1 | 1 | 2 | 4 | 2 | 1 | 3 | 1 | 2 |
| Web Spider (43) - D_801075F4 | 1 | 1 | 2 | 2 | 2 | 4 | 2 | 2 | 0 | 2 | 3 | 3 |
| Train Boss (45) - D_80107678 | 1 | 1 | 1 | 2 | 2 | 2 | 4 | 4 | 3 | 2 | 2 | 2 |
| Slash Beast (54) - D_801078F8 | 1 | 1 | 2 | 1 | 1 | 0 | 1 | 2 | 4 | 2 | 2 | 2 |
| Jet Stingray (56) - D_801079F8 | 1 | 1 | 2 | 2 | 2 | 1 | 1 | 2 | 0 | 4 | 2 | 0 |
| Frost Walrus (57) - D_80107A78 | 2 | 2 | 3 | 2 | 2 | 1 | 1 | 2 | 1 | 0 | 3 | 5 |
| Storm Owl (60) - D_80107B78 | 1 | 1 | 2 | 2 | 2 | 1 | 1 | 2 | 2 | 1 | 2 | 1 |
| Split Mushroom (61) - D_80107BF8 | 1 | 1 | 2 | 3 | 3 | 1 | 1 | 1 | 4 | 2 | 2 | 2 |
| Cyber Peacock (64) - D_80107CFC | 1 | 1 | 3 | 3 | 3 | 1 | 1 | 1 | 2 | 0 | 2 | 4 |
| Magma Dragoon (65) - D_80107D7C | 1 | 1 | 1 | 1 | 1 | 1 | 1 | 1 | 4 | 0 | 2 | 0 |
| Iris (66) - profile A - D_80107DFC | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 |
| Iris (66) - profile B - D_80107E84 | 2 | 2 | 3 | 3 | 3 | 2 | 2 | 3 | 1 | 2 | 2 | 4 |
| Sigma (68) - profile A - D_80107F84 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 6 |
| Sigma (68) - profile B - D_80108004 | 1 | 1 | 3 | 2 | 2 | 1 | 1 | 1 | 3 | 2 | 3 | 0 |
| Colonel (69) - D_80108084 | 1 | 1 | 1 | 3 | 3 | 2 | 2 | 2 | 0 | 4 | 2 | 2 |
| Drone Pod (70) - D_80108104 | 2 | 2 | 3 | 3 | 3 | 6 | 3 | 3 | 3 | 3 | 2 | 2 |
| Double (73) - D_80108204 | 0 | 3 | 0 | 2 | 0 | 2 | 0 | 2 | 0 | 0 | 0 | 0 |
| Sigma Final (74) - target 1 - D_80108284 | 0 | 0 | 0 | 2 | 2 | 1 | 0 | 2 | 0 | 2 | 3 | 4 |
| Sigma Final (74) - target 2 - D_80108304 | 1 | 2 | 2 | 2 | 2 | 2 | 0 | 2 | 0 | 3 | 2 | 0 |
| General (75) - profile A - D_80108384 | 2 | 2 | 4 | 3 | 3 | 3 | 3 | 3 | 3 | 2 | 4 | 2 |
| General (75) - profile B - D_80108404 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 |
