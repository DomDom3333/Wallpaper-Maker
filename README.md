# Wallpaper-Maker
A cross-platform Minimalist/Modern Art wallpaper generator built with **Avalonia UI** and **SkiaSharp**.

---

# Getting Started

1. Launch the application — your screen resolution is detected automatically.
2. Adjust the width/height fields if needed (or hit **Auto-Detect**).
3. Open **Settings** to choose shapes, fill styles, background, opacity, and strokes.
4. Open **Colors** to create or generate a color palette.
5. Select a palette from the dropdown and choose a supersampling level.
6. Hit **Generate** — a preview appears when the wallpaper is ready.
7. Click **Save** to export the image.

---

# Features

### Shapes — 17 types
| Original (seed-compatible) | New |
|---|---|
| Rectangle | Star |
| Square | Diamond |
| Ellipse | Cross |
| Circle | Arrow |
| Triangle | Rounded Rectangle |
| Pentagon | Curved Line |
| Hexagon | Blob |
| Octagon | Spiral |
| Hourglass | |

Each shape has independent **Amount** (1–9) and **Size** (1–9) sliders and can be toggled on/off individually.

### Rendering
- Powered by **SkiaSharp** for fast, high-quality rendering.
- **Supersampling / multisampling** — render at a higher internal resolution then downsample for crisp results at any target resolution.
- Shapes are drawn in a **randomised order** every generation, so even identical settings produce unique results due to layering.
- Full **anti-aliasing** on all shape types.

### Fill Modes (per shape)
- **Solid** — flat colour from the active palette.
- **Linear Gradient** — two palette colours blended at a random angle.
- **Radial Gradient** — two palette colours blended from the centre outward.

### Background Modes
- **Solid**
- **Linear Gradient**
- **Radial Gradient**

### Shape Appearance
- **Opacity** — independent minimum and maximum opacity sliders; each shape gets a random opacity in that range.
- **Strokes** — optional outline on every shape with a configurable stroke width.

### Color Palettes
- Unlimited pallets, unlimited colors per pallet.
- Pallets are **persisted automatically** to `%AppData%\WallpaperMaker\UserPallets.json` — no manual file management needed.
- **Color Theory palette generator** — generate harmonically correct palettes in one click:
  - Complementary
  - Analogous
  - Triadic
  - Split-Complementary
  - Tetradic
  - Monochromatic
  - Random (picks a harmony automatically)
- Generated pallets receive creative auto-generated names (e.g. *"Vibrant Ocean"*, *"Dusty Nebula"*).
- Create, rename, delete palettes and add/remove individual colours via the built-in **RGB colour picker**.

### Seed Sharing
Settings are encoded into a **27-character seed** (visible live in the Settings window). Share or paste a seed to reproduce the exact same shape configuration. Backward-compatible with seeds from the original WinForms release.

### Saving
Export the wallpaper in any of the following formats:
- PNG *(default)*
- JPEG
- BMP
- WebP


---

# Project Structure

| Project | Description |
|---|---|
| `WallpaperMaker.Domain` | Core logic — shapes, generator, colour theory, palette handling |
| `WallpaperMaker.Avalonia` | Cross-platform UI (Avalonia) — primary front-end |
| `WallpaperMaker.WinForm` | Legacy WinForms UI (Windows only) |
| `WallpaperMaker.Tests` | Unit tests (xUnit) |

---

#### Thanks
If you find any bugs or have ideas for improvements, feel free to create an issue — help is always welcome!
