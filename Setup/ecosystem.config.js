module.exports = {
  apps : [{
    name   : "azurite",
    script : "azurite start"
  }, {
    name   : "asrs-emulator",
    script : "asrs-emulator start ",
  }, 
  {
    name: "docker", 
    script: "docker start azuresqledge"
  }]
}
