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
                    writeFile file: 'C:\\inetpub\\wwwroot\\DISABLE-App_offline.htm', text: '''
                    <!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
                    <!-- your HTML content here -->
                    '''
                }
            }
        }
         

 stage('Database Synchronization') {
            steps {
                script {
                    // Source Database (AWS RDS)
                    def SOURCE_HOST = 'database-1.czy80ukqeckv.us-east-1.rds.amazonaws.com'
                    def SOURCE_USERNAME = 'admin'
                    def SOURCE_PASSWORD = 'admin123'
                    def SOURCE_DATABASE = 'employee'

                    // Destination Database (AWS RDS)
                    def DESTINATION_HOST = 'database-1.czy80ukqeckv.us-east-1.rds.amazonaws.com'
                    def DESTINATION_USERNAME = 'admin'
                    def DESTINATION_PASSWORD = 'admin123'
                    def DESTINATION_DATABASE = 'test_database'

                    // Dump data from source database
                    bat "mysqldump -h ${SOURCE_HOST} -u ${SOURCE_USERNAME} -p${SOURCE_PASSWORD} ${SOURCE_DATABASE} > source_dump.sql"

                    // Import data to destination database
                    bat "mysql -h ${DESTINATION_HOST} -u ${DESTINATION_USERNAME} -p${DESTINATION_PASSWORD} ${DESTINATION_DATABASE} < source_dump.sql"

                    // Cleanup: Remove the temporary dump file
                    bat "del source_dump.sql"
                }
            }
        }



//         stage('Database Synchronization') {
//             steps {
//                 script {
//                     try {
//                         def mysqlDumpCmd = "C:\\Program Files\\MySQL\\MySQL Workbench 8.0 CE\\mysqldump"
//                         def sourceUsername = "admin"
//                         def sourcePassword = "admin123"
//                         def sourceHost = "database-1.czy80ukqeckv.us-east-1.rds.amazonaws.com"
//                         def sourceDatabase = "employee"

//                         // MySQL dump from source database
//                         bat "${mysqlDumpCmd} -u ${sourceUsername} -p ${sourcePassword} -h ${sourceHost} ${sourceDatabase} > source_dump.sql"

//                         // MySQL import to destination database
//                         bat "mysql -u admin -p admin123 -h database-1.czy80ukqeckv.us-east-1.rds.amazonaws.com test_database < source_dump.sql"
//                         } catch (Exception e) {
//                             currentBuild.result = 'FAILURE'
//                             error "Failed to synchronize databases: ${e.message}"
//             }
//         }
//     }
// }




        //      stage('Database Synchronization') {
        //     steps {
        //         script {
        //             try {
        //                 // MySQL dump from source database
        //                 bat 'mysqldump -u admin -p admin123 -h database-1.czy80ukqeckv.us-east-1.rds.amazonaws.com employee > source_dump.sql'
                        
        //                 // MySQL import to destination database
        //                 bat 'mysql -u admin -p admin123 -h database-1.czy80ukqeckv.us-east-1.rds.amazonaws.com test_database < source_dump.sql'
        //             } catch (Exception e) {
        //                 currentBuild.result = 'FAILURE'
        //                 error "Failed to synchronize databases: ${e.message}"
        //             }
        //         }
        //     }
        // }
    }
}
