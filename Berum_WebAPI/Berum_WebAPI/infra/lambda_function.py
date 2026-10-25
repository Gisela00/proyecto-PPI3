import boto3
import os

ec2 = boto3.client('ec2')

def lambda_handler(event, context):
    instance_id = os.environ.get('INSTANCE_ID')
    
    action = even.get('action', '').lower()
    
    if not instance_id:
        return {"status": "error", "message": "Falta la variable de entorno INSTANCE_ID"}
   
    if action == 'start':
        ec2.start_instances(InstanceIds=[instance_id])
        return {"status": "success", "action": "start", "instance": instance_id}
   
    elif action == 'stop':
        ec2.stop_instances(InstanceIds=[instance_id])
        return {"status": "success", "action": "stop", "instance": instance_id}

    else:
        return {
            "status": "error",
            "message": "Acción no válida. Envía {'action': 'start'} o {'action': 'stop'}"
        }
