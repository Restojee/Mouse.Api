import { v4 } from 'uuid';
import * as path from 'path';
import { diskStorage } from 'multer';

const IMAGE_STATIC_PATH = './uploads/images';

export const imageStorage = diskStorage({
  destination: IMAGE_STATIC_PATH,
  filename: (request, file, callback) => {
    const filename: string = v4();
    const extension: string = path.parse(file.originalname).ext;

    callback(null, `${filename}${extension}`);
  },
});
