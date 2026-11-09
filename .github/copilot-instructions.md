# Instructions for coding agents

## Project

This repository is a matching decompilation of *Mega Man X4*. The C sources reconstruct the original PlayStation game, with byte-for-byte matching as a goal for US, JP, and EU builds. The same game code is also used by a native PC port; Saturn has a separate platform layer and is not byte-matching.

Key areas:

- `src/main`: reconstructed game logic shared by targets.
- `src/pc` and `src/saturn`: platform-specific layers.
- `include`: shared types, structures, and declarations.
- `config`: executable layouts and symbol data.
- `tests`: unit and integration tests.
- `tools`: build, decompilation, matching, and validation utilities.

See `README.md` for prerequisites and complete build instructions.

## Source of truth and translated references

- C files are the authoritative implementation of game behavior and binary layout.
- Individual C files may have neighboring `.cs` files containing conceptual C# translations for explanation, learning, or analysis. Such translations are supplementary references, not production PC implementations, and are not included in the C build.
- Do not assume every C file has a C# translation or that a translation is complete. Create or update one only when asked, and keep it clearly identified as a reference.
- When a C# reference conflicts with its C source, the C source wins. Do not use a reference translation as justification for changing game behavior.
- Preserve uncertainty: names such as `unk*`, address-based function names, and apparent oddities often reflect incomplete reverse engineering. Do not assign meanings or “clean up” behavior without evidence from the code or data.

### Object structures and RAM interpretation

- `include/obj.h` describes object data layouts and field types; its macros (such as `BASE_OBJ_FIELDS`, `MOVING_OBJ_FIELDS`, and `ANIMATED_OBJ_FIELDS`) expand fields into each structure. This is C data-layout reuse, not runtime class inheritance, allocation, or behavior.
- Use the concrete object structure and target platform to interpret field offsets. Shared-looking fields do not guarantee that every object uses the same structure or movement fields; for example, `MainObj` declares its own movement-related fields.
- `obj.h` provides offsets relative to an object, not an instance's absolute RAM address or a bus/physical address. To locate instances, inspect their pools, globals, symbols, and allocation/creation code. To map an address seen in an emulator, also establish the target platform and that emulator's address representation.
- When explaining RAM observations, distinguish field type/layout, object-relative offset, absolute address, and bus/physical address. Do not infer the latter two from `obj.h` alone.

### Expectations for C# reference translations

- Treat a requested C# file as a conceptual/pseudo-C# reference, not a production port. Follow neighboring reference-file conventions and clearly state that it is not included in the game's C build.
- Read the corresponding C implementation first and preserve its behavior, state transitions, update order, timer decrement counts, and edge cases. Keep unknown fields or meanings explicitly unknown instead of guessing.
- At the top of behavior-heavy classes, summarize the behavior states, the conditions and timing for transitions, and terminal outcomes. Keep lifecycle states distinct from behavior states when both exist.
- Make gameplay numbers readable: retain source values where useful, but add decimal equivalents beside hexadecimal timer, speed, acceleration, gravity, distance, and damage constants. For fixed-point values, explain the scale and units (for example, `65536 = 1 pixel/update`); clarify direction/sign and whether times count frames or multiple timer decrements.
- Associate animation indices with the behavior states that select them. Comment both the selection points and animation-table entries; call out defined animations that the source behavior does not select rather than implying they are used.
- Represent engine services through clear reference abstractions when needed, without claiming they are implemented by the C# model. Validate the reference's C# syntax independently when a suitable .NET SDK is available; C# reference files are not covered by the CMake build.

## Stage object creation

- Stage placements are stored as `StageObjectRecord` arrays in `src/main/stage_object_records*.c`. Each record contains `flags`, `id`, `subtype`, `object_type`, and the object's `x` and `y` coordinates.
- `src/main/stage_object_record_tables.c` indexes the arrays by stage and substage: `D_800F43C8` points to the full placement list, while `D_800F4430` points to the list position used when loading the current checkpoint. A record with `object_type == 0xFF` ends a list.
- At stage setup, `func_80028F58` in `src/main/195B4.c` creates objects at or before the current checkpoint by walking from `D_800F4430`. As the camera moves, `func_80028FEC` creates eligible records from `D_800F43C8` when their coordinates enter the camera bounds, then marks each spawned record to prevent duplicate creation.
- Both paths use `MakeObject(object_type)` to select an object pool, then copy the record's `id`, `subtype`, and coordinates to the allocated object.
- For main objects (`object_type == 0`), `id` selects the per-frame update function in `main_object_update_funcs` in `src/main/object_update.c`. For example, Cyber Space placements use ID `0x2A` (42), which dispatches to `homing_orb_update`. The placement record determines where and when an enemy is created; its ID determines which behavior updates it.

## Change and validation guidance

- Keep changes focused. Matching-sensitive C changes can alter generated executable bytes even when gameplay appears unchanged; avoid unrelated refactors and preserve existing compiler-oriented patterns.
- Check the relevant version, platform, callers, shared structures, and tests before changing behavior or data layouts.
- For PlayStation builds, follow the versioned workflow in `README.md`; `build.sh` validates output against `check.<version>.txt`.
- For the PC port, use the CMake workflow in `README.md`. Run the narrowest applicable tests; the project provides unit tests for the US PC configuration and PC scene tests.
- C# reference files are not covered by the CMake build. If editing one, validate its syntax independently when a suitable .NET SDK is available, and compare its behavior with the corresponding C source.
