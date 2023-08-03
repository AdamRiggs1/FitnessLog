import { getToken } from "./authManager";

const videoUrl = '/api/Video/'

export const getAllVideos = () => {
    return getToken().then((token) => {
        return fetch(`${videoUrl}`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`,
            },
        }).then((resp) => {
            if (resp.ok) {
                return resp.json();
            } else {
                throw new Error(
                    "An unknown error occurred while trying to get video.",
                );
            }
        });
    });
};

export const getVideobyId = (id) => {
    return getToken().then((token) => {
        return fetch(`${videoUrl}/${id}`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`,
            },
        }).then((resp) => {
            if (resp.ok) {
                return resp.json();
            } else {
                throw new Error(
                    "An unknown error occurred while trying to get video.",
                );
            }
        });
    });
};

export const addVideo = (video) => {
    return getToken().then((token) => {
        return fetch(videoUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(video)
        }).then((resp) => {
            if (resp.ok) {
                console.log("Video added successfully!")
                return resp.json();
            } else {
                throw new Error(
                    "An error occurred while trying to add video",
                );
            }
        });
    });
}

export const updateVideo = (video) => {
    return getToken().then((token) => {
        return fetch(videoUrl, {
            method: "PUT",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(video)
        }).then((resp) => {
            if (resp.ok) {
                console.log("Video edited successfully!")
                return resp.json();
            } else {
                throw new Error(
                    "An error occurred while trying to edit video",
                );
            }
        });
    });
}

export const deleteVideo = (id) => {
    return getToken().then(token => {
        return fetch(`${videoUrl}/${id}`, {
            method: "DELETE",
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
    })
}