﻿// Copyright (c) 2011 Blue Onion Software, All rights reserved
using BlueOnionSoftware;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell.Interop;
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class FontAndColorStorageTests
    {
        [Test]
        public void UpdateColorsUpdatesOutputWindowCategory()
        {
            try
            {
                var mockStore = new Mock<IVsFontAndColorStorage>();
                FontAndColorStorage.Override = mockStore.Object;
                FontAndColorStorage.UpdateColors();

                const uint flags = (uint)(
                    __FCSTORAGEFLAGS.FCSF_LOADDEFAULTS |
                        __FCSTORAGEFLAGS.FCSF_NOAUTOCOLORS |
                            __FCSTORAGEFLAGS.FCSF_PROPAGATECHANGES);

                var textEditorGuid = DefGuidList.guidTextEditorFontCategory;
                mockStore.Verify(s => s.OpenCategory(ref textEditorGuid, flags));
                mockStore.Verify(s => s.GetItem(OutputClassificationDefinitions.BuildHead, It.IsAny<ColorableItemInfo[]>()));
                mockStore.Verify(s => s.GetItem(OutputClassificationDefinitions.BuildText, It.IsAny<ColorableItemInfo[]>()));
                mockStore.Verify(s => s.GetItem(OutputClassificationDefinitions.LogInfo, It.IsAny<ColorableItemInfo[]>()));
                mockStore.Verify(s => s.GetItem(OutputClassificationDefinitions.LogWarn, It.IsAny<ColorableItemInfo[]>()));
                mockStore.Verify(s => s.GetItem(OutputClassificationDefinitions.LogError, It.IsAny<ColorableItemInfo[]>()));
                mockStore.Verify(s => s.GetItem(OutputClassificationDefinitions.LogSpecial, It.IsAny<ColorableItemInfo[]>()));
                mockStore.Verify(s => s.CloseCategory(), Times.Exactly(2));

                var ouputWindowGuid = DefGuidList.guidOutputWindowFontCategory;
                mockStore.Verify(s => s.OpenCategory(ref ouputWindowGuid, flags));
                mockStore.Verify(s => s.SetItem(OutputClassificationDefinitions.BuildHead, It.IsAny<ColorableItemInfo[]>()));
                mockStore.Verify(s => s.SetItem(OutputClassificationDefinitions.BuildText, It.IsAny<ColorableItemInfo[]>()));
                mockStore.Verify(s => s.SetItem(OutputClassificationDefinitions.LogInfo, It.IsAny<ColorableItemInfo[]>()));
                mockStore.Verify(s => s.SetItem(OutputClassificationDefinitions.LogWarn, It.IsAny<ColorableItemInfo[]>()));
                mockStore.Verify(s => s.SetItem(OutputClassificationDefinitions.LogError, It.IsAny<ColorableItemInfo[]>()));
                mockStore.Verify(s => s.SetItem(OutputClassificationDefinitions.LogSpecial, It.IsAny<ColorableItemInfo[]>()));
            }
            finally
            {
                FontAndColorStorage.Override = null;
            }
        }
    }
}