// Learn more about F# at http://fsharp.org

open System
open System.Windows.Forms
open System.Drawing
open System.Numerics
open Microsoft.FSharp.Math

[<EntryPoint>]
let main argv =
    printfn "hi"
    let cmax = Complex(1.0, 1.0)
    let cMin = Complex(-1.0,1.0)
    let rec isInMandlebtorSet (z:Complex,c:Complex,iter,count) =
        if (z.Real * z.Real + z.Imaginary * z.Imaginary <= 4.0 && count<iter) then
            isInMandlebtorSet( ((z*z)+c), c,iter, (count+1))
        else count
    let scalingFactor s = s * 1.0 / 200.0
    let mapPlane (x,y,s,mx,my) = 
        let fx = ((float x) * scalingFactor s) + mx
        let fy = ((float y) * scalingFactor s) + my
        Complex(fx,fy)
    let colorize c = 
        let r = (4 * c) % 255
        let g = (6 * c) % 255
        let b = (8 * c) % 255
        Color.FromArgb(r,g,b)
    let createImage (s, mx, my, iter) =
        let image = new Bitmap(400, 400)
        for x = 0 to image.Width - 1 do
            for y = 0 to image.Height - 1 do
                let count = isInMandelbrotSet( Complex.Zero, (mapPlane (x, y, s, mx, my)), iter, 0)
                if count = iter then
                    image.SetPixel(x,y, Color.Black)
                else
                    image.SetPixel(x,y, colorize( count ) )
        let temp = new Form() in
        temp.Paint.Add(fun e -> e.Graphics.DrawImage(image, 0, 0))
        temp
    0 // return an integer exit code
