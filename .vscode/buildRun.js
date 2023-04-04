const { exec } = require("child_process")
const fs = require("fs")

console.time("Finished")

exec("dotnet build --no-dependencies -c Release", (err, stdout, stderr) => {
    if (err == null) {
        fs.renameSync("bin/Release/net46/Shipshape.dll", "../Shipshape.dll")
        fs.rmSync("bin", { recursive: true, force: true })
        fs.rmSync("obj", { recursive: true, force: true })

        console.timeEnd("Finished")
        exec('"E:/Steam/steamapps/common/Landfall Archives/Archive/Airships 2/Airships 2.exe"', (err, stdout, stderr) => { })
    } else {
        throw stderr
    }
})