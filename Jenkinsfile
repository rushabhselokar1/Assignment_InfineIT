pipeline {
			agent any
			stages {
				stage('Source'){
					steps{
						checkout([$class: 'GitSCM', branches: [[name: '*/master']], doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [], userRemoteConfigs: [[credentialsId: 'c5451ba3-6250-4639-bfab-2ffc99306a4b', url: 'https://github.com/rushabhselokar1/Assignment_InfineIT.git']]])
					}
				}
				stage('Build') {
    					steps {
    					    bat "\"${tool 'MSBuild'}\" Assignment_InfineIT.sln /p:DeployOnBuild=true /p:DeployDefaultTarget=WebPublish /p:WebPublishMethod=FileSystem /p:SkipInvalidConfigurations=true /t:build /p:Configuration=Release /p:Platform=\"Any CPU\" /p:publishUrl=C:\\test12"
    					}
				}
			}
}