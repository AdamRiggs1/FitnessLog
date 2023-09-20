import { getToken } from "./authManager";

const userFoodUrl = '/api/UserFood/GetMyFood/'
const userFoodsUrl = '/api/UserFood/'

export const getAllFoods = () => {
    //fetches the getToken function, then inserts a token parameter
    return getToken().then((token) => {
        return fetch(userFoodUrl, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`,
            },
        }).then((resp) => {
            if (resp.ok) {
                return resp.json();
            } else {
                throw new Error(
                    "An unknown error occurred while trying to get foods.",
                );
            }
        });
    });
};

export const addUserFood = (userFood) => {
    return getToken().then((token) => {
        return fetch(userFoodsUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(userFood)
        }).then((resp) => {
            if (resp.ok) {
                console.log("Food added successfully!")
                return resp.json();
            } else {
                throw new Error(
                    "An error occurred while trying to add a food",
                );
            }
        });
    });
}

export const deleteUserFood = (id) => {
    return getToken().then(token => {
        return fetch(`${userFoodsUrl}/${id}`, {
            method: "DELETE",
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
    })
}