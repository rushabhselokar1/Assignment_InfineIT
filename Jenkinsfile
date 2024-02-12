pipeline {
    agent any
    
    environment {
        MSBUILD_PATH = "${tool 'MSBuild'}"
        PROJECT_PATH = 'C:\\ProgramData\\Jenkins\\.jenkins\\workspace\\pipeline\\Assignment_InfineIT\\Assignment_InfineIT.csproj'
        PUBLISH_PATH = 'C:\\test2'
    }

    stages {
    stage('Source'){
					steps{
						checkout([$class: 'GitSCM', branches: [[name: '*/master']], doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [], userRemoteConfigs: [[credentialsId: 'c5451ba3-6250-4639-bfab-2ffc99306a4b', url: 'https://github.com/rushabhselokar1/Assignment_InfineIT.git']]])
					}
				}

        stage('Build') {
            steps {
                script {
                    // MSBuild command to build the project
                    bat "\"${MSBUILD_PATH}\" ${PROJECT_PATH} /p:Configuration=Release"
                }
            }
        }

        stage('Publish') {
            steps {
                script {
                    // MSBuild command to publish the project to a specific path
                    bat "\"${MSBUILD_PATH}\" ${PROJECT_PATH} /p:DeployOnBuild=true /p:PublishProfile=FolderProfile /p:PublishDir=${PUBLISH_PATH}"
                }
            }
        }
    }
}