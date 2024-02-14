pipeline {
    agent any

    triggers {
        cron('*/2 * * * *')  // Trigger the build every hour from 10 AM to 6 PM on Monday to Saturday
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

//         stage('Run Windows Batch Commands') {
//     steps {
//         script {
//             try {
//                 echo "Running Windows Batch commands..."

//                 // Quote the paths to handle spaces and potential special characters
//                 def sourcePath = '"C:\\ProgramData\\Jenkins\\.jenkins\\workspace\\pipe\\Assignment_InfineIT\\obj\\Release\\Package"'
//                 def destinationPath = '"C:\\backup_publish_project"'

//                 // Use 'robocopy' to copy folders and files from source to destination
//                 bat "robocopy $sourcePath $destinationPath /E"
//             } catch (Exception e) {
//                 currentBuild.result = 'FAILURE'
//                 error "Failed to run Windows Batch Commands: ${e.message}"
//             }
//         }
//     }
// }


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
                    writeFile file: 'C:\\inetpub\\wwwroot\\DISABLE-App_offline.htm', text: '''
                    <!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
                    <html>
                    <head>
                        <style type="text/css">
                            @font-face {
                                font-family: "Open Sans";
                                font-style: normal;
                                font-weight: 400;
                                src: local("Segoe UI"), local("Open Sans"), local("OpenSans"), url(https://themes.googleusercontent.com/static/fonts/opensans/v6/K88pR3goAWT7BTt32Z01mz8E0i7KZn-EPnyo3HZu7kw.woff) format('woff');
                            }
                            body {
                                font-family: "Open Sans";
                            }
                            h1 {
                                font-size: 90px !important;
                            }
                            .error-page-container {
                                color: #333333;
                                margin: 50px auto 0;
                                text-align: center;
                                width: 600px;
                            }
                            .error-page-container h1 {
                                font-size: 120px;
                                font-weight: normal;
                                line-height: 120px;
                                margin: 10px 0;
                                font-family: "Open Sans";
                            }
                            .error-page-container h2 {
                                border-bottom: 1px solid #CCCCCC;
                                color: #666666;
                                font-size: 18px;
                                font-weight: normal;
                                font-family: "Open Sans";
                            }
                            .error-page-container a {
                                text-decoration: none;
                                color: #ffffff;
                                background-color: #009AD7;
                                padding: 11px 19px;
                            }
                            .error-page-container a:hover {
                                text-decoration: none;
                            }
                        </style>
                        <title>Under Maintenance</title>
                    </head>
                    <body>
                        <div class="error-page-container">
                            <h1>Maintenance</h1>
                            <img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAM...." alt="" />
                            <h2>
                                <p>Website is under maintenance right now. It will be back in few minutes.</p>
                            </h2>
                            <br />
                            <a href="/">Try again</a>
                        </div>
                    </body>
                    </html>
                    '''
                }
            }
        }
    }
}
