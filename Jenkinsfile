pipeline {
    agent any

    triggers {
        cron('0 10-18/1 * * 1-6')  // Trigger the build every hour from 10 AM to 6 PM on Monday to Saturday
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
                    // Create app_offline.htm for maintenance
                    writeFile file: 'C:\\inetpub\\wwwroot\\app_offline.htm', text: '<html><body><h1>Under Maintenance</h1><p>We\'ll be back soon!</p></body></html>'

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

        stage('Run Windows Batch Commands') {
            steps {
                script {
                    try {
                        // Example of running Windows Batch commands using the 'bat' step
                        bat '''
                            echo "Running Windows Batch commands..."
                            
                            rem Use 'robocopy' to copy folders and files from source to destination
                            robocopy "C:\\ProgramData\\Jenkins\\.jenkins\\workspace\\dev\\Assignment_InfineIT\\obj\\Release\\Package" "C:\\Tools" /E
                        '''
                    } catch (Exception e) {
                        currentBuild.result = 'FAILURE'
                        error "Failed to run Windows Batch Commands: ${e.message}"
                    }
                }
            }
        }

        stage('Archive Artifacts') {
            steps {
                // Archive artifacts with a specific destination path
                archiveArtifacts artifacts: '**/*', fingerprint: true, onlyIfSuccessful: true
            }
        }
              stage('Create app_offline.htm') {
                  steps {
                        script {
                               // Create app_offline.htm for maintenance
                                writeFile file: 'C:\\inetpub\\wwwroot\\app_offline.htm', text: '<html><body><h1>Under Maintenance</h1><p>We\'ll be back soon!</p></body></html>'
                }
            }
        }
    }
}
