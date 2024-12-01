def folderName = 'Devshift.Jwt'
def projectName = 'Devshift.Jwt'
def gitUrl = 'https://github.com/punnawutbu/Devshift.Jwt.git'
def gitBranch = 'refs/heads/master'
def publishProject = 'Devshift.Jwt/Devshift.Jwt.csproj'
def testProject = 'Devshift.Jwt.Tests'
def versionPrefix = "1.0"

folder(projectName)
pipelineJob("$projectName/Release") {
  logRotator(-1, 10)
  triggers {
    upstream("$projectName/Seed", 'SUCCESS')
  }
  definition {
    parameters {
      choiceParam('Release', ['Beta', 'General Availability (GA)'], '')
    }
    cps {
      sandbox()
      script("""
        @Library('jenkins-shared-libraries')_

        def _versionSuffix = Release == 'Beta' ? 'beta' : ''

        nuGetV2 {
          projectName = '$projectName'
          gitUri = '$gitUrl'
          gitBranch = '$gitBranch'
          publishProject = '$publishProject'
          testProject = '$testProject'
          versionPrefix = '$versionPrefix'
          versionSuffix = _versionSuffix
        }
     """)
    }
  }
}