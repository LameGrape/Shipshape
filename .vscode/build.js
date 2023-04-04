const { exec } = require("child_process")
const fs = require("fs")

console.time("Finished")

exec("dotnet build --no-dependencies -c Release", (err, stdout, stderr) => {
    if (err == null) {
        fs.renameSync("bin/Release/net46/Shipshape.dll", "../Shipshape.dll")
        fs.rmSync("bin", { recursive: true, force: true })
        fs.rmSync("obj", { recursive: true, force: true })

        console.timeEnd("Finished")
        process.exit()
    } else {
        throw "Failed to build"
    }
})