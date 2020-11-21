# Wallpaper-Maker
This is a WinForms based Wallpaper Generator that make Minimalist/Modern Art style wallpapers.

# Samples
https://imgur.com/gallery/y0OOdmI

# Getting started:
- Open the exe and punch in your resolution (in case it didnt autodetect)
- Open the settings tab to customize what kind of shapes you want to have
- Open the colors tab and make yourself a Color Pallet to use
- Select your color pallet on the main screen and hit 'Generate'
- There you go! Now save the image using the Save button.

# Features:
- Multisampling for even higher Resolutions!
- Flexible Color Pallet options that allow for unlimited Pallets and Colors per Pallet
- Shapes get drawn in random order, meaning even with the same settings, it will never look the same due to layering
- Import lots of Pallets at once by creating a /Resources/ColorPallets.json file and using the following format
```
{
  "Pallets": [
    {
      "Pallet": {
        "Name": "your pallet name",     /* change this to something unique*/
        "Colors":[                      /*add your colors here*/
          "229,244,227",
          "93,169,233",
          "0,63,145",
          "255,255,255",
          "109,50,109"
        ]
      }
    }
  ]
}
```
# Planned Features
- Seed sharing! Type in a manual seed to copy settings easily.
- Element Exporting! Save the Layout of the wallpaper elements to recreate the same layout with different colors.
- More Shapes! So many more shapes!






#### Thanks
If you find any bugs or have ideas for improovements, feel free to create an issiue. help ALWAYS wanted!
