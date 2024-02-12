pipeline {

    agent any
    stages {
		stage('Source'){
            			steps{
						checkout([$class: 'GitSCM', branches: [[name: '*/master']], doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [], userRemoteConfigs: [[credentialsId: 'c5451ba3-6250-4639-bfab-2ffc99306a4b', url: 'https://github.com/rushabhselokar1/Assignment_InfineIT.git']]])
					}
				}

            stage('Restore Packages') {
                steps {
                    script {
                        def nugetCmd = "C:\\ProgramData\\chocolatey\\bin\\nuget.exe"
                        def solutionFile = "C:\\ProgramData\\Jenkins\\.jenkins\\workspace\\pipeline\\Assignment_InfineIT.sln"
                    
                        bat "${nugetCmd} restore ${solutionFile}"
                    }
                }

            stage('Build') {
                steps {
                    script {
                        def msbuildCmd = "\"C:\\Program Files\\Microsoft Visual Studio\\2022\\Enterprise\\MSBuild\\Current\\Bin\\MSBuild.exe\""
                        def solutionFile = "C:\\ProgramData\\Jenkins\\.jenkins\\workspace\\pipeline\\Assignment_InfineIT.sln"
                    
                        bat "${msbuildCmd} ${solutionFile} /p:DeployOnBuild=true /p:DeployDefaultTarget=WebPublish /p:WebPublishMethod=FileSystem /p:SkipInvalidConfigurations=true /t:build /p:Configuration=Release /p:Platform=\"Any CPU\" /p:publishUrl=\"C:\\test12\""
                    }
                }
            }
        }
    }
}