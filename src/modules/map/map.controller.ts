import {
  ApiBearerAuth,
  ApiBody,
  ApiConsumes,
  ApiOperation,
  ApiResponse,
  ApiTags
} from '@nestjs/swagger';
import {
  Body,
  Controller,
  Delete,
  Get,
  HttpCode,
  HttpStatus,
  Param,
  Post,
  Put,
  Query,
  Request,
  UploadedFile,
  UseGuards,
  UseInterceptors,
} from '@nestjs/common';
import { JwtAuthGuard } from '../auth/guards/jwt-auth.guard';
import { MapService } from './map.service';
import { MapCreateRequest} from './models/map-create-request';
import { MapUpdateRequest } from './models/map-update-request';
import { FileInterceptor } from '@nestjs/platform-express';
import { MediaUpload } from '../media/models/media-upload';
import { imageStorage } from '../media/utils/imageStorage';
import { MapsGetRequest } from './models/maps-get-request';
import { Map } from './models/map';
import { imageHttpFilter } from "../media/utils/imageFilter";

@ApiTags('Maps')
@Controller('maps')
@ApiBearerAuth()
export class MapController {
  constructor(private mapService: MapService) {}

  @Get()
  @HttpCode(HttpStatus.OK)
  @ApiResponse({ status: HttpStatus.OK, type: Map, isArray: true })
  @ApiOperation({ operationId: 'GetMaps' })
  async getMaps(@Query() mapGetRequest: MapsGetRequest) {
    return await this.mapService.getMaps(mapGetRequest);
  }

  @Get(':id')
  @HttpCode(HttpStatus.OK)
  @ApiResponse({ status: HttpStatus.OK, type: Map })
  @ApiOperation({ operationId: 'GetMap' })
  async getMap(@Param('id') id: number) {
    return await this.mapService.getMap(id);
  }

  @Post()
  @UseGuards(JwtAuthGuard)
  @HttpCode(HttpStatus.CREATED)
  @ApiResponse({ status: HttpStatus.CREATED, type: Map })
  @ApiOperation({ operationId: 'CreateMap' })
  async create(
      @Body() mapCreateRequest: MapCreateRequest,
      @Request() request
  ) {
    return await this.mapService.createMap({
      userId: request.user.id,
      name: mapCreateRequest.name,
      description: mapCreateRequest.description
    });
  }

  @Put()
  @UseGuards(JwtAuthGuard)
  @ApiResponse({ status: HttpStatus.OK, type: Map })
  @ApiOperation({ operationId: 'UpdateMap' })
  async update(@Body() mapUpdateRequest: MapUpdateRequest) {
    return await this.mapService.updateMap(mapUpdateRequest);
  }

  @Post(':id/UploadMapImage')
  @UseGuards(JwtAuthGuard)
  @ApiOperation({ operationId: 'UploadMapImage' })
  @UseInterceptors(
    FileInterceptor('file', {
      storage: imageStorage,
      fileFilter: imageHttpFilter
    })
  )
  @ApiConsumes('multipart/form-data')
  @ApiBody({ type: MediaUpload })
  @ApiResponse({
    status: HttpStatus.OK,
    type: Map
  })
  @HttpCode(HttpStatus.OK)
  async uploadMapImage(
      @UploadedFile() file: Express.Multer.File,
      @Request() request,
      @Param('id') mapId: number
  ) {
    return await this.mapService.uploadMapImage({ mapId, image: file.filename });
  }

  @Post(':id/Complete')
  @UseGuards(JwtAuthGuard)
  @ApiOperation({ operationId: 'Complete' })
  @UseInterceptors(
    FileInterceptor('file', {
      storage: imageStorage,
      fileFilter: imageHttpFilter
    })
  )
  @ApiConsumes('multipart/form-data')
  @ApiBody({ type: MediaUpload })
  @ApiResponse({
    status: HttpStatus.OK,
    type: Map
  })
  @HttpCode(HttpStatus.OK)
  async CompleteMap(
      @UploadedFile() file: Express.Multer.File,
      @Request() request,
      @Param('id') id: number
  ) {
    return await this.mapService.completeMap({
      mapId: id,
      image: file.filename,
      userId: request.user.id
    });
  }

  @Put(':id/Favorite')
  @UseGuards(JwtAuthGuard)
  @ApiResponse({ status: HttpStatus.OK, type: Map })
  @ApiOperation({ operationId: 'Favorite' })
  async favoriteMap(@Param('id') mapId: number, @Request() request) {
    const { id: userId } = request.user;
    return await this.mapService.favoriteMap({ mapId, userId });
  }

  @Delete(':id/UnFavorite')
  @UseGuards(JwtAuthGuard)
  @ApiResponse({ status: HttpStatus.OK, type: Map })
  @ApiOperation({ operationId: 'UnFavorite' })
  async unFavoriteMap(@Param('id') mapId: number, @Request() request) {
    const { id: userId } = request.user;
    return await this.mapService.unFavoriteMap({ mapId, userId });
  }
}
