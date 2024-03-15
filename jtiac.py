import requests
import sseclient  # pip install sseclient-py
import json

def callia():
    url = "http://10.120.29.149:5000/v1/chat/completions"

    headers = {
        "Content-Type": "application/json"
    }

    headers = {
    "Content-Type": "application/json"
    }

    history = []

    while True:
        user_message = input("> ")
        if input == '0':
            break
        history.append({"role": "user", "content": user_message})
        data = {
            "mode": "chat",
            "character": "Example",
            "messages": history
        }

        response = requests.post(url, headers=headers, json=data, verify=False)
        assistant_message = response.json()['choices'][0]['message']['content']
        history.append({"role": "assistant", "content": assistant_message})
        print(assistant_message)

callia()