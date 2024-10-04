-- Check for blocking sessions
SELECT 
    blocking_session_id, 
    session_id, 
    wait_type, 
    wait_time, 
    wait_resource
FROM sys.dm_exec_requests
WHERE blocking_session_id <> 0;
