pipeline {
    agent any

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
                    writeFile file: 'C:\\inetpub\\wwwroot\\App_offline.htm', text: '''
                    <!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
                    <html>
                    <head>
                        <style type="text/css">
                            /* ... CSS Styles ... */
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
                        def sourceUsername = "admin"
                        def sourcePassword = "admin123"
                        def sourceHost = "database-1.czy80ukqeckv.us-east-1.rds.amazonaws.com"
                        def sourceDatabase = "employee"

                        // MySQL dump from source database
                        bat "${mysqlDumpCmd} -u ${sourceUsername} -p${sourcePassword} -h ${sourceHost} ${sourceDatabase} > source_dump.sql"

                        // MySQL import to destination database
                        bat "${mysqlCmd} -u admin -padmin123 -h database-1.czy80ukqeckv.us-east-1.rds.amazonaws.com test_database2 < source_dump.sql"
                    } catch (Exception e) {
                        currentBuild.result = 'FAILURE'
                        error "Failed to synchronize databases: ${e.message}"
                    }
                }
            }
        }
    }
}
