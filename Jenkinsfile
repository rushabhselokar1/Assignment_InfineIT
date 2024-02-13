pipeline {
    agent any

    triggers {
        cron('H 10-18/1 * * 1-6')  // Trigger the build every hour from 10 AM to 6 PM on weekdays
    }

    stages {
        stage('Source') {
            steps {
                checkout([$class: 'GitSCM', branches: [[name: '*/master']], doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [], userRemoteConfigs: [[credentialsId: 'c5451ba3-6250-4639-bfab-2ffc99306a4b', url: 'https://github.com/rushabhselokar1/Assignment_InfineIT.git']]])
            }
        }

        stage('Build') {
            steps {
                script {
                    // Define MSBuild command
                    def msbuildCmd = "\"${tool 'MSBuild'}\" Assignment_InfineIT.sln " +
                                    "/p:DeployOnBuild=true " +
                                    "/p:DeployDefaultTarget=WebPublish " +
                                    "/p:WebPublishMethod=FileSystem " +
                                    "/p:SkipInvalidConfigurations=true " +
                                    "/t:build " +
                                    "/p:Configuration=Release " +
                                    "/p:Platform=\"Any CPU\" " +
                                    "/p:DeleteExistingFiles=True " +
                                    "/p:publishUrl=c:\\inetpub\\wwwroot"

                    // Execute MSBuild and additional commands in a single bat step
                    bat """
                        ${msbuildCmd}
                        echo Additional commands...
                        rem Add your additional commands here
                    """
                }
            }
        }

        stage('Run Bash Script') {
            steps {
                // Example of running a Bash script using the 'sh' step
                sh '''
                    echo "Running Windows Bash script..."
                    # Add your Windows Bash commands here

                    # Note: This example uses 'rsync' instead of 'robocopy' since 'robocopy' is Windows-specific
                    rsync -a C:/ProgramData/Jenkins/.jenkins/workspace/pipe/Assignment_InfineIT/obj/Release/ C:/inetpub/wwwroot
                '''
            }
        }

        stage('Archive Artifacts') {
            steps {
                // Archive artifacts with a specific destination path
                archiveArtifacts artifacts: '**/*', fingerprint: true, onlyIfSuccessful: true
            }
        }
    }
}
