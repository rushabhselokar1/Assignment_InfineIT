pipeline {
    agent any

    // triggers {
    //     cron('*/2 * * * *')  // Trigger the build every 2 min
    // }

    stages {
        stage('Source') {
            steps {
                checkout([$class: 'GitSCM', branches: [[name: '*/master']], doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [], userRemoteConfigs: [[credentialsId: '4722ac02-04cb-4f6e-a1b8-4398701e2e60', url: 'https://github.com/rushabhselokar1/Assignment_InfineIT.git']]])
            }
        }

        stage('Build') {
            steps {
                script {
                    // NuGet Package Restore
                    bat "\"C:\\Program Files (x86)\\NuGet\\nuget.exe\" restore Assignment_InfineIT.sln"
                    
                    // MSBuild command
                    def msbuildCmd = "\"C:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Community\\MSBuild\\Current\\Bin\\MSBuild.exe\" Assignment_InfineIT.sln " +
                                    "/p:DeployOnBuild=true " +
                                    "/p:DeployDefaultTarget=WebPublish " +
                                    "/p:WebPublishMethod=FileSystem " +
                                    "/p:SkipInvalidConfigurations=true " +
                                    "/t:build " +
                                    "/p:Configuration=Release " +
                                    "/p:Platform=\"Any CPU\" " +
                                    "/p:DeleteExistingFiles=True " +
                                    "/p:publishUrl=C:\\inetpub\\wwwroot"

                    // Execute NuGet Package Restore, MSBuild, and additional commands in a single bat step
                    bat """
                        ${msbuildCmd}
                        echo Additional commands...
                        rem Add your additional commands here
                    """
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
         
        stage('Database Synchronization') {
            steps {
                script {
                    try {
                        def mysqlDumpCmd = "C:\\xampp\\mysql\\bin\\mysqldump"
                        def mysqlCmd = "C:\\xampp\\mysql\\bin\\mysql"
                        def sourceUsername = "root"
                        def sourceHost = "127.0.0.1"
                        def sourceDatabase = "database2"

                        // MySQL dump from source database
                        bat "${mysqlDumpCmd} -u ${sourceUsername} -h ${sourceHost} ${sourceDatabase} > source_dump.sql"

                        // MySQL import to destination database
                        bat "${mysqlCmd} -u root -h 127.0.0.1 emp < source_dump.sql"

                        // // MySQL import to destination database 2
                        // bat "${mysqlCmd} -u root -h 127.0.0.1 database2 < source_dump.sql"
                   
                        // // MySQL import to destination database 2
                        // bat "${mysqlCmd} -u root -h 127.0.0.1 test < source_dump.sql"

                    } catch (Exception e) {
                        currentBuild.result = 'FAILURE'
                        error "Failed to synchronize databases: ${e.message}"
                    }
                }
            }
        }

//         stage('Perform Database Synchronization') {
//         steps {
//         script {
//             // Export data and schema from the source database
//             sh "mysqldump -u root -h 127.0.0.1 --no-data employee > source_schema.sql"
//             sh "mysqldump -u root -h 127.0.0.1 employee > source_data.sql"

//             // Drop and recreate the destination database
//             sh "mysql -u root -h 127.0.0.1 -e \"DROP DATABASE IF EXISTS database1; CREATE DATABASE database1;\""

//             // Import schema into the destination database
//             sh "mysql -u root -h 127.0.0.1 database1 < source_schema.sql"

//             // Import data into the destination database with data overwrite
//             sh "mysql -u root -h 127.0.0.1 --force database1 < source_data.sql"
//         }
//     }
// }
    }
}