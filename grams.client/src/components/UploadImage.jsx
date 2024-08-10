import React, { useState } from 'react';
import axios from 'axios';

const UploadImage = () => {
    const [file, setFile] = useState(null);
    const [message, setMessage] = useState('');

    const onFileChange = (e) => {
        setFile(e.target.files[0]);
    };

    const onUpload = async () => {
        if (!file) {
            setMessage('Please select a file first.');
            return;
        }

        const formData = new FormData();
        formData.append('file', file);

        try {
            const response = await axios.post('https://localhost:7099/api/Image', formData, {
                headers: { 'Content-Type': 'multipart/form-data' }
            });
            setMessage('File uploaded successfully!');
            if (onUploadSuccess) {
                onUploadSuccess(); // Call the callback to refresh the image list
            }
        } catch (error) {
            console.error('Upload Error:', error.response ? error.response.data : error.message);
            setMessage('File upload failed.');
        }
    };

    return (
        <div>
            <input type="file" onChange={onFileChange} />
            <button onClick={onUpload}>Upload</button>
            <p>{message}</p>
        </div>
    );
};

export default UploadImage;
