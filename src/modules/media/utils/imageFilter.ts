import { HttpException, HttpStatus } from '@nestjs/common';
import { extname } from 'path/win32';

const MAX_IMAGE_SIZE = 512 * 1024;

const AVAILABLE_EXTENSION_REG_EXP = /\/(jpg|jpeg|png|gif)$/;

export const imageHttpFilter = (request, file, callback) => {

  if (!file.mimetype.match(AVAILABLE_EXTENSION_REG_EXP)) {
    callback(
        new HttpException(
            `Неподдерживаемый тип файла - ${ extname(file.originalname) }`,
            HttpStatus.BAD_REQUEST,
        )
    )
  }

  if (file.size > MAX_IMAGE_SIZE) {
    callback(
        new HttpException(
            `Размер файла не може превышать 512кб`,
            HttpStatus.BAD_REQUEST,
        )
    )
  }

  callback(null, true);
};
