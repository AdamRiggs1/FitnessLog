import { getToken } from "./authManager";

const foodUrl = '/api/Food/'

export const getFoodbyId = (id) => {
    return getToken().then((token) => {
        return fetch(`${foodUrl}/${id}`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`,
            },
        }).then((resp) => {
            if (resp.ok) {
                return resp.json();
            } else {
                throw new Error(
                    "An unknown error occurred while trying to get food.",
                );
            }
        });
    });
};

export const addFood = (food) => {
    return getToken().then((token) => {
        return fetch(foodUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(food)
        }).then((resp) => {
            if (resp.ok) {
                console.log("Food added successfully!")
                return resp.json();
            } else {
                throw new Error(
                    "An error occurred while trying to add food",
                );
            }
        });
    });
}

export const updateFood = (food) => {
    return getToken().then((token) => {
        return fetch(foodUrl, {
            method: "PUT",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(food)
        }).then((resp) => {
            if (resp.ok) {
                console.log("Food edited successfully!")
                return resp.json();
            } else {
                throw new Error(
                    "An error occurred while trying to edit food",
                );
            }
        });
    });
}

export const deleteStrengthWorkout = (id) => {
    return getToken().then(token => {
        return fetch(`${foodUrl}/${id}`, {
            method: "DELETE",
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
    })
}
