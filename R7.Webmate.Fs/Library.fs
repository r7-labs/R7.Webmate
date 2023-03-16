namespace R7.Webmate.Text.Models

open System

type public DecodeHexStringModel() =
    
    let mutable hexString = ""
    member this.HexString
        with get () = hexString
        and set (value: string) =
            let hexString2 = if value.StartsWith("0x") then value.Substring(2) else value    
            let hexString3 = if hexString2.Length % 2 = 1 then hexString2.Substring(0, hexString2.Length - 1) else hexString2
            hexString <- hexString3

    member this.ConvertToByte(hex: string) =
        try Convert.ToByte(hex, 16) with | _ -> Convert.ToByte(0)

    member this.Process() =
        let bytes: byte[] = Array.zeroCreate(this.HexString.Length / 2)
        for i in 0 .. bytes.Length - 1 do
            let hex = this.HexString.Substring(i * 2, 2)
            Array.set bytes i (this.ConvertToByte hex)
        bytes
