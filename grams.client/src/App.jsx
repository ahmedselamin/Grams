import React from 'react';
import UploadImage from './components/UploadImage';
import ImageList from './components/ImageList';

import './App.css'

function App() {
    return (
        <div className="App">
            <h1>Image Upload and Display</h1>
            <UploadImage />
            <ImageList />
        </div>
    );
}

export default App;
